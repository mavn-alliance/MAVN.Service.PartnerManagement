using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MAVN.Service.PartnerManagement.Client.Api;
using MAVN.Service.PartnerManagement.Client.Enums;
using MAVN.Service.PartnerManagement.Client.Models.Partner;
using MAVN.Service.PartnerManagement.Client.Models.PartnerLinking;
using MAVN.Service.PartnerManagement.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace MAVN.Service.PartnerManagement.Controllers
{
    [Route("api/linking")]
    [ApiController]
    public class PartnerLinkingController : ControllerBase, ILinkingApi
    {
        private readonly IPartnerLinkingService _partnerLinkingService;
        private readonly IMapper _mapper;

        public PartnerLinkingController(IPartnerLinkingService partnerLinkingService, IMapper mapper)
        {
            _partnerLinkingService = partnerLinkingService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets partner linking info
        /// </summary>
        /// <param name="partnerId">partner id.</param>
        /// <response code="200">Partner lining info.</response>
        [HttpGet("info")]
        [ProducesResponseType(typeof(PartnerLinkingInfoResponse), (int)HttpStatusCode.OK)]
        public async Task<PartnerLinkingInfoResponse> GetPartnerLinkingInfoAsync([FromQuery] Guid partnerId)
        {
            var result = await _partnerLinkingService.GetOrAddPartnerLinkingInfoAsync(partnerId);

            return _mapper.Map<PartnerLinkingInfoResponse>(result);
        }

        /// <summary>
        /// Regenerates partner linking info
        /// </summary>
        /// <param name="partnerId">partner id.</param>
        [HttpPost("info")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task RegeneratePartnerLinkingInfoAsync([FromBody] Guid partnerId)
        {
            await _partnerLinkingService.AddOrUpdatePartnerLinkingInfoAsync(partnerId);
        }

        /// <summary>
        /// Links a partner to a customer
        /// </summary>
        /// <param name="request">Request model</param>
        [HttpPost]
        [ProducesResponseType(typeof(LinkPartnerResponse), (int)HttpStatusCode.OK)]
        public async Task<LinkPartnerResponse> LinkPartnerAsync([FromBody] LinkPartnerRequest request)
        {
            var result = await _partnerLinkingService.LinkPartnerAndCustomerAsync(request.CustomerId,
                request.PartnerCode, request.PartnerLinkingCode);

            return new LinkPartnerResponse
            {
                Error = (PartnerLinkingErrorCode)result
            };
        }

        /// <summary>
        /// Returns linked partner if exists
        /// </summary>
        /// <param name="customerId">Id of the customer</param>
        [HttpGet]
        [ProducesResponseType(typeof(Guid?), (int)HttpStatusCode.OK)]
        public async Task<Guid?> GetLinkedPartnerAsync([FromQuery] Guid customerId)
        {
            var result = await _partnerLinkingService.GetLinkedPartnerAsync(customerId);

            return result;
        }
    }
}
