using System;
using MAVN.Service.PartnerManagement.Domain.Constants;

namespace MAVN.Service.PartnerManagement.DomainServices.Helpers
{
    public static class DistanceHelper
    {
        private const double PiDividedBy180 = 0.017453292519943295;

        public static int GetGeohashLevelByRadius(double radiusInKm)
        {
            var diameterInKm = radiusInKm * 2;
            var dict = GeohashLevelsSmallerSidesInKm.GeohashLevelsWithSmallerSidesInKm;

            for (var i = dict.Count; i >= 1; i--)
            {
                if (dict[i-1] >= diameterInKm)
                    return i;
            }

            return 1;
        }

        public static double GetDistanceInKmBetweenTwoPoints(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; // Radius of the earth in km
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in km
            return d;
        }

        private static double ToRadians(double deg)
            => deg * PiDividedBy180;
    }
}
