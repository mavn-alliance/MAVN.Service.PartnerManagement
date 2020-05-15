using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Common.Log;
using Geohash;
using Lykke.Common.ApiLibrary.Exceptions;
using Lykke.Common.Log;
using MAVN.Service.Credentials.Client;
using MAVN.Service.Credentials.Client.Models.Requests;
using MAVN.Service.Credentials.Client.Models.Responses;
using MAVN.Service.CustomerProfile.Client;
using MAVN.Service.PartnerManagement.Domain.Exceptions;
using MAVN.Service.PartnerManagement.Domain.Models;
using MAVN.Service.PartnerManagement.Domain.Models.Dto;
using MAVN.Service.PartnerManagement.Domain.Repositories;
using MAVN.Service.PartnerManagement.Domain.Services;
using MoreLinq;

namespace MAVN.Service.PartnerManagement.DomainServices
{
    public class PartnerService: IPartnerService
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly ILocationService _locationService;
        private readonly ICredentialsClient _credentialsClient;
        private readonly ICustomerProfileClient _customerProfileClient;
        private readonly IMapper _mapper;
        private readonly Geohasher _geohasher;
        private readonly ILog _log;

        public PartnerService(
            IPartnerRepository partnerRepository, 
            ILocationService locationService,
            ICredentialsClient credentialsClient,
            ICustomerProfileClient customerProfileClient,
            IMapper mapper,
            ILogFactory logFactory)
        {
            _partnerRepository = partnerRepository;
            _locationService = locationService;
            _credentialsClient = credentialsClient;
            _customerProfileClient = customerProfileClient;
            _mapper = mapper;
            _log = logFactory.CreateLog(this);
            _geohasher = new Geohasher();
        }

        public async Task<Guid> CreateAsync(Partner partner)
        {
            partner.Id = Guid.NewGuid();

            partner.Locations = await _locationService.CreateLocationsContactPersonForPartnerAsync(partner);

            if (partner.UseGlobalCurrencyRate)
            {
                partner.AmountInTokens = null;
                partner.AmountInCurrency = null;
            }

            CredentialsCreateResponse credential;

            try
            {
                credential = await _credentialsClient.Partners.CreateAsync(new PartnerCredentialsCreateRequest
                {
                    PartnerId = partner.Id.ToString(),
                    ClientId = partner.ClientId,
                    ClientSecret = partner.ClientSecret
                });
            }
            catch (ClientApiException e)
            {
                if (e.HttpStatusCode == HttpStatusCode.BadRequest)
                {
                    throw new PartnerRegistrationFailedException(e.ErrorResponse.ErrorMessage);
                }

                throw;
            }

            if (credential.Error == CredentialsError.LoginAlreadyExists)
            {
                var exception = new ClientAlreadyExistException($"Client with id '{partner.ClientId}' already exist.");
                _log.Warning(exception.Message, context: partner);
                throw exception;
            }

            if (credential.Error != CredentialsError.None)
            {
                var exception = new PartnerRegistrationFailedException("Register user failed.");
                _log.Warning(exception.Message, context: partner);
                throw exception;
            }

            var createdPartner = await _partnerRepository.CreateAsync(partner);

            return createdPartner.Id;
        }

        public async Task UpdateAsync(Partner partner)
        {
            var existingPartner = await _partnerRepository.GetByIdAsync(partner.Id);

            if (existingPartner == null)
            {
                throw new PartnerNotFoundFailedException($"Partner with id '{partner.Id}' not found.");
            }

            if (string.IsNullOrEmpty(partner.ClientId))
            {
                partner.ClientId = existingPartner.ClientId;
            }

            if (!string.IsNullOrEmpty(partner.ClientSecret))
            {
                CredentialsUpdateResponse credentialUpdate;

                try
                {
                    credentialUpdate = await _credentialsClient.Partners.UpdateAsync(new PartnerCredentialsUpdateRequest
                    {
                        PartnerId = partner.Id.ToString(),
                        ClientId = partner.ClientId,
                        ClientSecret = partner.ClientSecret
                    });
                }
                catch (ClientApiException e)
                {
                    if (e.HttpStatusCode == HttpStatusCode.BadRequest)
                    {
                        throw new PartnerRegistrationFailedException(e.ErrorResponse.ErrorMessage);
                    }

                    throw;
                }

                if (credentialUpdate.Error == CredentialsError.LoginAlreadyExists)
                {
                    var exception = new ClientAlreadyExistException($"Client with id '{partner.ClientId}' already exist.");
                    _log.Warning(exception.Message, context: partner);
                    throw exception;
                }

                if (credentialUpdate.Error != CredentialsError.None)
                {
                    var exception = new PartnerRegistrationUpdateFailedException($"Updating the credentials for partner {partner.Id} failed.");
                    _log.Warning(exception.Message, context: partner);
                    throw exception;
                }
            }

            if (partner.UseGlobalCurrencyRate)
            {
                partner.AmountInTokens = null;
                partner.AmountInCurrency = null;
            }
            
            partner.CreatedAt = existingPartner.CreatedAt;
            partner.CreatedBy = existingPartner.CreatedBy;

            partner.Locations = await _locationService.UpdateRangeAsync(partner, partner.Locations, existingPartner.Locations);

            await _partnerRepository.UpdateAsync(partner);
        }

        public async Task DeleteAsync(Guid partnerId)
        {
            var partner = await _partnerRepository.GetByIdAsync(partnerId);

            if (partner == null)
            {
                return;
            }

            var asyncActions = new List<Task>();
            partner.Locations?.ForEach(l => { asyncActions.Add(DeleteContactPerson(l)); });

            await Task.WhenAll(asyncActions);

            await _partnerRepository.DeleteAsync(partnerId);
        }

        public async Task<(IReadOnlyCollection<Partner> partners, int totalSize)> GetAsync(PartnerListRequestDto model)
        {
            return await _partnerRepository.GetAsync(model);
        }

        public async Task<Partner> GetByIdAsync(Guid partnerId)
        {
            var partner = await _partnerRepository.GetByIdAsync(partnerId);

            return await EnrichPartner(partner);
        }

        public async Task<Partner> GetByClientIdAsync(string clientId)
        {
            var partner = await _partnerRepository.GetByClientIdAsync(clientId);

            return await EnrichPartner(partner);
        }

        public Task<IReadOnlyCollection<Partner>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            var partners = _partnerRepository.GetByIdsAsync(ids);

            return partners;
        }

        public async Task<Partner> GetByLocationIdAsync(Guid locationId)
        {
            var partner = await _partnerRepository.GetByLocationIdAsync(locationId);

            return await EnrichPartner(partner);
        }

        public async Task<Guid[]> GetNearPartnerIdsByCoordinatesAndGeohashLevelAsync(short geohashLevel, double longitude, double latitude)
        {
            if(geohashLevel < 1 || geohashLevel > 9 || latitude < -90 || latitude > 90 || longitude < -180 || longitude > 80)
                throw new ArgumentException("Invalid argument value for get near partners request");

            var geohash = _geohasher.Encode(latitude, longitude, precision: geohashLevel);
            var result = await _partnerRepository.GetPartnerIdsByGeohashAsync(geohash);
            return result;
        }

        private async Task<Partner> EnrichPartner(Partner partner)
        {
            if (partner == null)
            {
                return null;
            }

            var asyncActions = new List<Task>();
            partner.Locations?.ForEach(l => { asyncActions.Add(GetContactPerson(l)); });

            await Task.WhenAll(asyncActions);

            return partner;
        }

        private async Task GetContactPerson(Location location)
        {
            var partnerContact = await _customerProfileClient.PartnerContact.GetByLocationIdAsync(location.Id.ToString());
            location.ContactPerson = _mapper.Map<ContactPerson>(partnerContact);
        }

        private async Task DeleteContactPerson(Location location)
        {
            await _customerProfileClient.PartnerContact.DeleteAsync(location.Id.ToString());
            location.ContactPerson = null;
        }
    }
}
