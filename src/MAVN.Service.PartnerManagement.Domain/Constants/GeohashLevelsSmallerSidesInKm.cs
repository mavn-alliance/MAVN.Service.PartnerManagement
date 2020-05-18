using System.Collections.Generic;

namespace MAVN.Service.PartnerManagement.Domain.Constants
{
    /// <summary>
    /// Holds values of the smaller sized side of the geohash rectangle
    /// More info about geohash https://www.elastic.co/guide/en/elasticsearch/guide/current/geohashes.html#geohashes
    /// </summary>
    public class GeohashLevelsSmallerSidesInKm
    {
        public static readonly Dictionary<int, double> GeohashLevelsWithSmallerSidesInKm =
            new Dictionary<int, double>
            {
                {1, 5.004},
                {2, 625},
                {3, 156},
                {4, 19.5},
                {5, 4.9},
                {6, 0.61},
                {7, 0.1528},
                {8, 0.0191},
                {9, 0.00478},
            };
    }
}
