using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Lykke.Common.Api.Contract.Responses;
using Lykke.Service.PartnerManagement.Client.Api;
using Lykke.Service.PartnerManagement.Client.Models.Authentication;
using Lykke.Service.PartnerManagement.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Lykke.Service.PartnerManagement.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller, IAuthApi
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        /// <response code="200">AuthenticateResponseModel.</response>
        /// <response code="400">ErrorResponse.</response>
        [HttpPost("login")]
        [SwaggerOperation("Login")]
        [ProducesResponseType(typeof(AuthenticateResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<AuthenticateResponseModel> AuthenticateAsync([FromBody] AuthenticateRequestModel request)
        {
            var authModel = await _authService.AuthAsync(request.ClientId, request.ClientSecret, request.UserInfo);

            return _mapper.Map<AuthenticateResponseModel>(authModel);
        }

        /// <inheritdoc/>
        /// <response code="200">string.</response>
        [HttpPost("generateClientId")]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
        public async Task<string> GenerateClientId()
        {
            return await _authService.GenerateClientId();
        }

        /// <inheritdoc/>
        /// <response code="200">string.</response>
        [HttpPost("generateClientSecret")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<string> GenerateClientSecret()
        {
            return await _authService.GenerateClientSecret();
        }
    }
}
