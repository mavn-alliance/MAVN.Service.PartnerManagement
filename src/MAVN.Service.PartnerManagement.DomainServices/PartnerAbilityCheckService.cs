using System;
using System.Threading.Tasks;
using MAVN.Service.Kyc.Client;
using MAVN.Service.Kyc.Client.Models.Enums;
using MAVN.Service.PartnerManagement.Domain.Models.Enums;
using MAVN.Service.PartnerManagement.Domain.Services;
using MAVN.Service.PaymentManagement.Client;
using MAVN.Service.PaymentManagement.Client.Models.Requests;
using MAVN.Service.PaymentManagement.Client.Models.Responses;

namespace MAVN.Service.PartnerManagement.DomainServices
{
    public class PartnerAbilityCheckService : IPartnerAbilityCheckService
    {
        private readonly IPaymentManagementClient _paymentManagementClient;
        private readonly IKycClient _kycClient;

        public PartnerAbilityCheckService(IPaymentManagementClient paymentManagementClient, IKycClient kycClient)
        {
            _paymentManagementClient = paymentManagementClient;
            _kycClient = kycClient;
        }

        public async Task<PartnerInabilityErrorCodes> CheckPartnerAbility(PartnerAbility abilityToCheck, Guid partnerId)
        {
            switch (abilityToCheck)
            {
                case PartnerAbility.PublishSmartVoucherCampaign:
                    var paymentIntegrationResponse =
                        await _paymentManagementClient.Api.CheckPaymentIntegrationAsync(
                            new PaymentIntegrationCheckRequest { PartnerId = partnerId });

                    if (paymentIntegrationResponse != CheckPaymentIntegrationErrorCode.None)
                        return PartnerInabilityErrorCodes.InvalidPaymentIntegrationDetails;

                    var kycStatus = await _kycClient.KycApi.GetCurrentByPartnerIdAsync(partnerId);
                    if (kycStatus == null || kycStatus.KycStatus != KycStatus.Accepted)
                        return PartnerInabilityErrorCodes.KycNotPassed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return PartnerInabilityErrorCodes.None;
        }
    }
}
