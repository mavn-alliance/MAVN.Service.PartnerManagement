namespace MAVN.Service.PartnerManagement.Domain.Constants
{
    /// <summary>
    /// Holds values of the smaller sized side of the geohash rectangle
    /// More info about geohash https://www.elastic.co/guide/en/elasticsearch/guide/current/geohashes.html#geohashes
    /// </summary>
    public class GeohashLevelsSmallerSidesInKm
    {
        public const double Level1 = 5.004;
        public const double Level2 = 625;
        public const double Level3 = 156;
        public const double Level4 = 19.5;
        public const double Level5 = 4.9;
        /// More levels could be added if needed
        public const double Level6 = 0.61;
    }
}
