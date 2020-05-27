using System;
using System.Linq;
using System.Threading.Tasks;
using MAVN.Service.CustomerProfile.Client;
using MAVN.Service.PartnerManagement.Domain.Enums;
using MAVN.Service.PartnerManagement.Domain.Models;
using MAVN.Service.PartnerManagement.Domain.Repositories;
using MAVN.Service.PartnerManagement.Domain.Services;

namespace MAVN.Service.PartnerManagement.DomainServices
{
    public class PartnerLinkingService : IPartnerLinkingService
    {
        private const string AlphanumericSymbols = "abcdefghijklmnopqrstuvwxyz0123456789";

        private readonly IPartnerLinkingRepository _partnerLinkingRepository;
        private readonly ICustomerProfileClient _customerProfileClient;

        public PartnerLinkingService(IPartnerLinkingRepository partnerLinkingRepository, ICustomerProfileClient customerProfileClient)
        {
            _partnerLinkingRepository = partnerLinkingRepository;
            _customerProfileClient = customerProfileClient;
        }

        public async Task<IPartnerLinkingInfo> GetOrAddPartnerLinkingInfoAsync(Guid partnerId)
        {
            var existingPartnerLinkingInfo = await _partnerLinkingRepository.GetPartnerLinkingInfoAsync(partnerId);

            if (existingPartnerLinkingInfo != null)
                return existingPartnerLinkingInfo;

            var partnerLinkingInfo = await GeneratePartnerLinkingInfo(partnerId);

            await _partnerLinkingRepository.AddPartnerLinkingInfoAsync(partnerLinkingInfo);

            return partnerLinkingInfo;
        }

        public async Task AddOrUpdatePartnerLinkingInfoAsync(Guid partnerId)
        {
            var partnerLinkingInfo = await GeneratePartnerLinkingInfo(partnerId);

            await _partnerLinkingRepository.AddOrUpdatePartnerLinkingInfoAsync(partnerLinkingInfo);
        }

        public async Task<PartnerLinkingErrorCodes> LinkPartnerAndCustomerAsync(Guid customerId, string partnerCode, string partnerLinkingCode)
        {
            var customer = await _customerProfileClient.CustomerProfiles.GetByCustomerIdAsync(customerId.ToString());

            if (customer?.Profile == null)
                return PartnerLinkingErrorCodes.CustomerDoesNotExist;

            if (await _partnerLinkingRepository.CustomerHasLinkAsync(customerId))
                return PartnerLinkingErrorCodes.CustomerAlreadyLinked;

            var partnerLinkingInfo =
                await _partnerLinkingRepository.GetPartnerLinkingInfoByPartnerCodeAsync(partnerCode);

            if (partnerLinkingInfo == null)
                return PartnerLinkingErrorCodes.PartnerLinkingInfoDoesNotExist;

            if (partnerLinkingInfo.PartnerLinkingCode != partnerLinkingCode)
                return PartnerLinkingErrorCodes.PartnerLinkingInfoDoesNotMatch;

            await _partnerLinkingRepository.AddPartnerLinkAsync(new LinkedPartner
            {
                PartnerId = partnerLinkingInfo.PartnerId,
                CustomerId = customerId,
            });

            return PartnerLinkingErrorCodes.None;
        }

        public Task<Guid?> GetLinkedPartnerAsync(Guid customerId)
            => _partnerLinkingRepository.GetLinkedPartnerByCustomerIdAsync(customerId);

        private async Task<PartnerLinkingInfo> GeneratePartnerLinkingInfo(Guid partnerId)
        {
            var partnerLinkingInfo = new PartnerLinkingInfo
            {
                PartnerId = partnerId,
                PartnerLinkingCode = GenerateAlphanumericCode(),
            };

            var partnerCode = GenerateAlphanumericCode();

            var codeExists = true;
            while (codeExists)
            {
                codeExists = await _partnerLinkingRepository.PartnerCodeExistsAsync(partnerCode);

                if (codeExists)
                    partnerCode = GenerateAlphanumericCode();
            }

            partnerLinkingInfo.PartnerCode = partnerCode;
            return partnerLinkingInfo;
        }

        private string GenerateAlphanumericCode(int codeLength = 8)
        {
            var random = new Random();

            return new string(Enumerable.Repeat(AlphanumericSymbols, codeLength)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
        }
    }
}
