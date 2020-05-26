using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Common.Log;
using Lykke.Common.Log;
using MAVN.Service.PartnerManagement.Client.Api;
using MAVN.Service.PartnerManagement.Client.Enums;
using MAVN.Service.PartnerManagement.Client.Models.Partner;
using MAVN.Service.PartnerManagement.Domain.Exceptions;
using MAVN.Service.PartnerManagement.Domain.Models;
using MAVN.Service.PartnerManagement.Domain.Models.Dto;
using MAVN.Service.PartnerManagement.Domain.Services;
using MAVN.Service.PaymentManagement.Client;
using MAVN.Service.PaymentManagement.Client.Models.Requests;
using MAVN.Service.PaymentManagement.Client.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MAVN.Service.PartnerManagement.Controllers
{
    [Route("api/partners")]
    [ApiController]
    public class PartnersController : Controller, IPartnersApi
    {
        private readonly IPartnerService _partnerService;
        private readonly IPaymentManagementClient _paymentManagementClient;
        private readonly IMapper _mapper;
        private readonly ILog _log;

        public PartnersController(
            IPartnerService partnerService,
            IPaymentManagementClient paymentManagementClient,
            IMapper mapper,
            ILogFactory logFactory)
        {
            _partnerService = partnerService;
            _paymentManagementClient = paymentManagementClient;
            _mapper = mapper;
            _log = logFactory.CreateLog(this);
        }

        /// <summary>
        /// Gets all partners paginated.
        /// </summary>
        /// <param name="request">The paginated list request parameters.</param>
        /// <response code="200">A collection of partners.</response>
        [HttpGet]
        [ProducesResponseType(typeof(PartnerListResponseModel), (int) HttpStatusCode.OK)]
        public async Task<PartnerListResponseModel> GetAsync([FromQuery] PartnerListRequestModel request)
        {
            var result = await _partnerService.GetAsync(_mapper.Map<PartnerListRequestDto>(request));

            return new PartnerListResponseModel
            {
                CurrentPage = request.CurrentPage,
                TotalSize = result.totalSize,
                PartnersDetails = _mapper.Map<IReadOnlyCollection<PartnerListDetailsModel>>(result.partners)
            };
        }

        /// <summary>
        /// Gets ids of partners which are close to passed longitude and latitude
        /// </summary>
        /// <param name="request">request parameters.</param>
        /// <response code="200">A collection of partner ids.</response>
        [HttpGet("byCoordinates")]
        [ProducesResponseType(typeof(GetNearPartnersByCoordinatesResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<GetNearPartnersByCoordinatesResponse> GetNearPartnerByCoordinatesAsync([FromQuery] GetNearPartnersByCoordinatesRequest request)
        {
            var result = await _partnerService.GetNearPartnerIdsAsync(request.RadiusInKm, request.Longitude, request.Latitude, request.CountryIso3Code);

            return new GetNearPartnersByCoordinatesResponse
            {
                PartnersIds = result
            };
        }

        /// <summary>
        /// Check if partner has ability to do something
        /// </summary>
        /// <param name="request">.</param>
        /// <response code="200">Check ability response.</response>
        [HttpGet("ability/check")]
        [ProducesResponseType(typeof(CheckAbilityResponse), (int)HttpStatusCode.OK)]
        public async Task<CheckAbilityResponse> CheckAbilityAsync([FromQuery] CheckAbilityRequest request)
        {
            switch (request.PartnerAbility.Value)
            {
                case PartnerAbility.PublishSmartVoucherCampaign:
                    var result =
                        await _paymentManagementClient.Api.CheckPaymentIntegrationAsync(
                            new PaymentIntegrationCheckRequest { PartnerId = request.PartnerId });
                    return new CheckAbilityResponse
                    {
                        HasAbility = result == CheckPaymentIntegrationErrorCode.None,
                        ErrorCode = result != CheckPaymentIntegrationErrorCode.None
                            ? PartnerInabilityErrorCodes.InvalidPaymentIntegrationDetails
                            : PartnerInabilityErrorCodes.None
                    };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Gets partner by identifier.
        /// </summary>
        /// <param name="id">The partner identifier.</param>
        /// <response code="200">A detailed partner information.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PartnerDetailsModel), (int) HttpStatusCode.OK)]
        public async Task<PartnerDetailsModel> GetByIdAsync(Guid id)
        {
            var result = await _partnerService.GetByIdAsync(id);

            return _mapper.Map<PartnerDetailsModel>(result);
        }

        /// <summary>
        /// Gets partner by client identifier.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <response code="200">A detailed partner information.</response>
        [HttpGet("byClientId/{clientId}")]
        [ProducesResponseType(typeof(PartnerDetailsModel), (int) HttpStatusCode.OK)]
        public async Task<PartnerDetailsModel> GetByClientIdAsync(string clientId)
        {
            var result = await _partnerService.GetByClientIdAsync(clientId);

            return _mapper.Map<PartnerDetailsModel>(result);
        }

        /// <summary>
        /// Gets partner by location identifier.
        /// </summary>
        /// <param name="locationId">The location identifier.</param>
        /// <response code="200">A detailed partner information.</response>
        [HttpGet("byLocationId/{locationId}")]
        [ProducesResponseType(typeof(PartnerDetailsModel), (int) HttpStatusCode.OK)]
        public async Task<PartnerDetailsModel> GetByLocationIdAsync(Guid locationId)
        {
            var result = await _partnerService.GetByLocationIdAsync(locationId);

            return _mapper.Map<PartnerDetailsModel>(result);
        }

        /// <summary>
        /// Creates partner.
        /// </summary>
        /// <param name="partnerCreateModel">The partner creation details.</param>
        /// <response code="200">The result of partner creation.</response>
        [HttpPost]
        [ProducesResponseType(typeof(PartnerCreateResponse), (int) HttpStatusCode.OK)]
        public async Task<PartnerCreateResponse> CreateAsync(PartnerCreateModel partnerCreateModel)
        {
            Guid id;
            try
            {
                id = await _partnerService.CreateAsync(_mapper.Map<Partner>(partnerCreateModel));
            }
            catch (ClientAlreadyExistException e)
            {
                _log.Info(e.Message, partnerCreateModel);

                return new PartnerCreateResponse
                {
                    ErrorCode = PartnerManagementError.AlreadyRegistered, ErrorMessage = e.Message
                };
            }
            catch (PartnerRegistrationFailedException e)
            {
                _log.Info(e.Message, partnerCreateModel);

                return new PartnerCreateResponse
                {
                    ErrorCode = PartnerManagementError.RegistrationFailed, ErrorMessage = e.Message
                };
            }
            catch (LocationContactRegistrationFailedException e)
            {
                _log.Info(e.Message, partnerCreateModel);

                return new PartnerCreateResponse
                {
                    ErrorCode = PartnerManagementError.RegistrationFailed, ErrorMessage = e.Message
                };
            }
            catch (LocationExternalIdNotUniqueException e)
            {
                _log.Info(e.Message, partnerCreateModel);

                return new PartnerCreateResponse
                {
                    ErrorCode = PartnerManagementError.LocationExternalIdNotUnique, ErrorMessage = e.Message
                };
            }

            return new PartnerCreateResponse {Id = id, ErrorCode = PartnerManagementError.None};
        }

        /// <summary>
        /// Updates partner.
        /// </summary>
        /// <param name="partnerUpdateModel">The partner update details.</param>
        /// <response code="200">The result of partner update.</response>
        [HttpPut]
        [ProducesResponseType(typeof(PartnerUpdateResponse), (int) HttpStatusCode.OK)]
        public async Task<PartnerUpdateResponse> UpdateAsync([FromBody] PartnerUpdateModel partnerUpdateModel)
        {
            try
            {
                await _partnerService.UpdateAsync(_mapper.Map<Partner>(partnerUpdateModel));
            }
            catch (PartnerNotFoundFailedException e)
            {
                _log.Info(e.Message, partnerUpdateModel);

                return new PartnerUpdateResponse
                {
                    ErrorCode = PartnerManagementError.PartnerNotFound, ErrorMessage = e.Message
                };
            }
            catch (ClientAlreadyExistException e)
            {
                _log.Info(e.Message, partnerUpdateModel);

                return new PartnerUpdateResponse
                {
                    ErrorCode = PartnerManagementError.AlreadyRegistered, ErrorMessage = e.Message
                };
            }
            catch (PartnerRegistrationFailedException e)
            {
                _log.Info(e.Message, partnerUpdateModel);

                return new PartnerUpdateResponse
                {
                    ErrorCode = PartnerManagementError.RegistrationFailed, ErrorMessage = e.Message
                };
            }
            catch (LocationContactUpdateFailedException e)
            {
                _log.Info(e.Message, partnerUpdateModel);

                return new PartnerUpdateResponse
                {
                    ErrorCode = PartnerManagementError.RegistrationFailed, ErrorMessage = e.Message
                };
            }
            catch (LocationContactRegistrationFailedException e)
            {
                _log.Info(e.Message, partnerUpdateModel);

                return new PartnerUpdateResponse
                {
                    ErrorCode = PartnerManagementError.RegistrationFailed, ErrorMessage = e.Message
                };
            }
            catch (LocationExternalIdNotUniqueException e)
            {
                _log.Info(e.Message, partnerUpdateModel);

                return new PartnerUpdateResponse
                {
                    ErrorCode = PartnerManagementError.LocationExternalIdNotUnique, ErrorMessage = e.Message
                };
            }

            return new PartnerUpdateResponse();
        }

        /// <summary>
        /// Deletes partner by identifier.
        /// </summary>
        /// <param name="id">The partner identifier.</param>
        /// <response code="204">The partner successfully deleted.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.NoContent)]
        public async Task DeleteAsync(Guid id)
        {
            await _partnerService.DeleteAsync(id);
        }

        /// <summary>
        /// Gets partners by collect of identifier.
        /// </summary>
        /// <param name="ids">The partners identifiers.</param>
        /// <response code="200">A collection of partners</response>
        [HttpPost("list")]
        [ProducesResponseType(typeof(IReadOnlyCollection<PartnerListDetailsModel>), (int)HttpStatusCode.OK)]
        public async Task<IReadOnlyCollection<PartnerListDetailsModel>> GetByIdsAsync([FromBody] Guid[] ids)
        {
            var result = await _partnerService.GetByIdsAsync(ids);

            return _mapper.Map<IReadOnlyCollection<PartnerListDetailsModel>>(result);
        }

    }
}
