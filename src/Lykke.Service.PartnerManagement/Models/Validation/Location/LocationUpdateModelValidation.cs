using System;
using FluentValidation;
using Lykke.Service.PartnerManagement.Client.Models.Location;

namespace Lykke.Service.PartnerManagement.Models.Validation.Location
{
    public class LocationUpdateModelValidation : LocationBaseModelValidation<LocationUpdateModel>
    {
        public LocationUpdateModelValidation(): base()
        {
        }
    }
}
