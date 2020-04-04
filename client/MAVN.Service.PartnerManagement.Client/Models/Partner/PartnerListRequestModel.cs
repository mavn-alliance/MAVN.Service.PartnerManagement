namespace MAVN.Service.PartnerManagement.Client.Models.Partner
{
    /// <summary>
    /// Represents a model user for list request
    /// </summary>
    public class PartnerListRequestModel
    {
        /// <summary>
        /// The requested page
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// The requested page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Name for filtering - optional
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Vertical for filtering - optional
        /// </summary>
        public Vertical? Vertical { get; set; }
    }
}
