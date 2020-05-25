using System.Globalization;

namespace MAVN.Service.PartnerManagement.DomainServices.Helpers
{
    public static class DoubleExtensions
    {
        public static string ToDotString(this double value)
            => value.ToString(NumberFormatInfo.InvariantInfo);
    }
}
