using System;
using System.Net;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.ApiLibrary.Exceptions;
using Lykke.Common.Log;
using Lykke.Service.Credentials.Client;
using Lykke.Service.Credentials.Client.Models.Requests;
using Lykke.Service.Credentials.Client.Models.Responses;
using MAVN.Service.PartnerManagement.Domain.Exceptions;
using MAVN.Service.PartnerManagement.Domain.Models;
using MAVN.Service.PartnerManagement.Domain.Services;
using Lykke.Service.Sessions.Client;
using Lykke.Service.Sessions.Client.Models;

namespace MAVN.Service.PartnerManagement.DomainServices
{
    public class AuthService: IAuthService
    {
        private readonly ICredentialsClient _credentialsClient;
        private readonly ISessionsServiceClient _sessionsServiceClient;
        private readonly int _sessionsServiceTokenTimeToLiveInSeconds;
        private readonly int _usernameLength;
        private readonly int _passwordLength;
        private readonly ILog _log;

        public AuthService(
            ILogFactory logFactory, 
            ICredentialsClient credentialsClient, 
            ISessionsServiceClient sessionsServiceClient,
            int sessionsServiceTokenTimeToLiveInSeconds,
            int usernameLength,
            int passwordLength)
        {
            _credentialsClient = credentialsClient;
            _sessionsServiceClient = sessionsServiceClient;
            _sessionsServiceTokenTimeToLiveInSeconds = sessionsServiceTokenTimeToLiveInSeconds;
            _usernameLength = usernameLength;
            _passwordLength = passwordLength;
            _log = logFactory.CreateLog(this);
        }

        public async Task<AuthResult> AuthAsync(string clientId, string clientSecret, string userInfo)
        {
            PartnerCredentialsValidationResponse credentials;

            try
            {
                _log.Info("Partner credentials validation started", new { clientId, userInfo });

                credentials = await _credentialsClient.Partners.ValidateAsync(
                    new PartnerCredentialsValidationRequest { ClientSecret = clientSecret, ClientId = clientId });

                _log.Info("Partner credentials validation finished", new { clientId, userInfo });
            }
            catch (ClientApiException e) when (e.HttpStatusCode == HttpStatusCode.BadRequest)
            {
                _log.Info("Partner credentials validation failed", new { clientId, userInfo }, e);

                return new AuthResult { Error = ServicesError.InvalidCredentials };
            }

            if (credentials.Error != CredentialsError.None)
            {
                switch (credentials.Error)
                {
                    case CredentialsError.LoginNotFound:
                        return new AuthResult { Error = ServicesError.LoginNotFound };

                    case CredentialsError.PasswordMismatch:
                        return new AuthResult { Error = ServicesError.PasswordMismatch };
                }

                var exc = new InvalidOperationException(
                    $"Unexpected error during partner credentials validation for {clientId}");

                _log.Error(exc, context: new { credentials.Error, clientId, userInfo });

                throw exc;
            }

            _log.Info("Partner authentication started", new { clientId, userInfo });

            var session = await _sessionsServiceClient.SessionsApi.AuthenticateAsync(credentials.PartnerId,
                new CreateSessionRequest { Ttl = TimeSpan.FromSeconds(_sessionsServiceTokenTimeToLiveInSeconds) });

            _log.Info("Partner authentication finished", new { clientId, userInfo });

            return new AuthResult { Token = session.SessionToken };
        }

        public async Task<string> GenerateClientId()
        {
            var clientId = await _credentialsClient.Api.GenerateClientIdAsync(new GenerateClientIdRequest {Length = _usernameLength});
            var clientSecret = await _credentialsClient.Api.GenerateClientSecretAsync(new GenerateClientSecretRequest {Length = _passwordLength});

            var retries = 0;
            while (true)
            {
                var result = await _credentialsClient.Partners.ValidateAsync(new PartnerCredentialsValidationRequest
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                });

                if (result.Error == CredentialsError.LoginAlreadyExists)
                {
                    // Generating existing user 20 times is highly unlikely so we throw exception
                    if (retries >= 20)
                    {
                        var exception = new PartnerRegistrationFailedException("Register user failed.");
                        _log.Error(exception);
                        throw exception;
                    }

                    clientId = await _credentialsClient.Api.GenerateClientIdAsync(new GenerateClientIdRequest {Length = _usernameLength});

                    retries++;
                    continue;
                }

                break;
            }

            return clientId;
        }

        public async Task<string> GenerateClientSecret()
        {
            return await _credentialsClient.Api.GenerateClientSecretAsync(new GenerateClientSecretRequest {Length = _passwordLength});
        }
    }
}
