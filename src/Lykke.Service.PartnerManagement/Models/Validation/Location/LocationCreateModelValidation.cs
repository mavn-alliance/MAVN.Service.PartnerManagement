using JetBrains.Annotations;
using Lykke.Service.PartnerManagement.Client.Models.Location;

namespace Lykke.Service.PartnerManagement.Models.Validation.Location
{
    [UsedImplicitly]
    public class LocationCreateModelValidation : LocationBaseModelValidation<LocationCreateModel>
    {
        public LocationCreateModelValidation(): base()
        {
        }
    }
}
