using System;

namespace MAVN.Service.PartnerManagement.Contract
{
    /// <summary>
    /// Event fired when partner is created in the system
    /// </summary>
    public class PartnerCreatedEvent
    {
        /// <summary>
        /// Id of the newly created partner
        /// </summary>
        public Guid PartnerId { get; set; }
        /// <summary>
        /// The id of the user who created the partner
        /// </summary>
        public Guid CreatedBy { get; set; }
        /// <summary>
        /// Timestamp of the event
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
