using Lykke.Service.PartnerManagement.Client.Enums;

namespace Lykke.Service.PartnerManagement.Client.Models
{
    /// <summary>
    /// Represents PartnerManagementErrorResponseModel model 
    /// </summary>
    public class PartnerManagementErrorResponseModel
    {
        /// <summary>
        /// Represents error code from the operation.
        /// </summary>
        public PartnerManagementError ErrorCode { get; set; }

        /// <summary>
        /// Represents error message from the operation.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
