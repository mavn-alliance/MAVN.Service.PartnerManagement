using System.Collections.Generic;

namespace MAVN.Service.PartnerManagement.Domain.Constants
{
    /// <summary>
    /// Holds values of the smaller sized side of the geohash rectangle
    /// More info about geohash https://www.elastic.co/guide/en/elasticsearch/guide/current/geohashes.html#geohashes
    /// </summary>
    public class GeohashLevelsSmallerSidesInKm
    {
        public static readonly List <double> GeohashLevelsWithSmallerSidesInKm =
            new List<double>
            {
                //level 1
                5.004,
                //level 2
                625,
                //level 3
                156,
                //level 4
                19.5,
                //level 5
                4.9,
                //level 6
                0.61,
                //level 7
                0.1528,
                //level 8
                0.0191,
                //level 9
                0.00478,
            };
    }
}
