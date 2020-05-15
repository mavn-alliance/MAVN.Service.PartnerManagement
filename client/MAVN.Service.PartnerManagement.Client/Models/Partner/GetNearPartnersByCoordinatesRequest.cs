namespace MAVN.Service.PartnerManagement.Client.Models.Partner
{
    /// <summary>
    /// Request model
    /// </summary>
    public class GetNearPartnersByCoordinatesRequest
    {
        /// <summary>
        /// The latitude you want to search around
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// The longitude you want to search around
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// The geohash level used for the search 
        /// </summary>
        public short GeohashLevel { get; set; } = 5;
    }
}
