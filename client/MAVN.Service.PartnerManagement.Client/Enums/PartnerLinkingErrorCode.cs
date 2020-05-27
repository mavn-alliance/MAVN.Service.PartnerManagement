namespace MAVN.Service.PartnerManagement.Client.Enums
{
    /// <summary>
    /// Holds error codes for partner linking
    /// </summary>
    public enum PartnerLinkingErrorCode
    {
        /// <summary>
        /// No error
        /// </summary>
        None,
        /// <summary>
        /// Customer does not exist
        /// </summary>
        CustomerDoesNotExist,
        /// <summary>
        /// This customer is already linked
        /// </summary>
        CustomerAlreadyLinked,
        /// <summary>
        /// Partner linking info is missing
        /// </summary>
        PartnerLinkingInfoDoesNotExist,
        /// <summary>
        /// The properties from the linking info does not match
        /// </summary>
        PartnerLinkingInfoDoesNotMatch,
    }
}
