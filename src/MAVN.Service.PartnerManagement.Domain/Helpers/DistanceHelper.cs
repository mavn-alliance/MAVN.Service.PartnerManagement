using System;
using MAVN.Service.PartnerManagement.Domain.Constants;

namespace MAVN.Service.PartnerManagement.Domain.Helpers
{
    public static class DistanceHelper
    {
        public static short GetGeohashLevelByRadius(int radiusInKm)
        {
            var diameterInKm = radiusInKm * 2;

            if (diameterInKm <= GeohashLevelsSmallerSidesInKm.Level5 &&
                diameterInKm > GeohashLevelsSmallerSidesInKm.Level6)
                return 5;

            if (diameterInKm <= GeohashLevelsSmallerSidesInKm.Level4 &&
                diameterInKm > GeohashLevelsSmallerSidesInKm.Level5)
                return 4;

            if (diameterInKm <= GeohashLevelsSmallerSidesInKm.Level3 &&
                diameterInKm > GeohashLevelsSmallerSidesInKm.Level4)
                return 3;

            if (diameterInKm <= GeohashLevelsSmallerSidesInKm.Level2 &&
                diameterInKm > GeohashLevelsSmallerSidesInKm.Level3)
                return 2;

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
        {
            return deg * (Math.PI / 180);
        }
    }
}
