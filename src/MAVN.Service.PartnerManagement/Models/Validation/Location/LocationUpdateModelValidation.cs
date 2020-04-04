using System;
using FluentValidation;
using MAVN.Service.PartnerManagement.Client.Models.Location;

namespace MAVN.Service.PartnerManagement.Models.Validation.Location
{
    public class LocationUpdateModelValidation : LocationBaseModelValidation<LocationUpdateModel>
    {
        public LocationUpdateModelValidation(): base()
        {
        }
    }
}
