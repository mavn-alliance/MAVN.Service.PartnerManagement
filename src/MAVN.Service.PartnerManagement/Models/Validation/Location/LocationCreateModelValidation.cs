using JetBrains.Annotations;
using MAVN.Service.PartnerManagement.Client.Models.Location;

namespace MAVN.Service.PartnerManagement.Models.Validation.Location
{
    [UsedImplicitly]
    public class LocationCreateModelValidation : LocationBaseModelValidation<LocationCreateModel>
    {
        public LocationCreateModelValidation(): base()
        {
        }
    }
}
