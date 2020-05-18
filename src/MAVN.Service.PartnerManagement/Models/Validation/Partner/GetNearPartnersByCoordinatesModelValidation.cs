using FluentValidation;
using MAVN.Service.PartnerManagement.Client.Models.Partner;

namespace MAVN.Service.PartnerManagement.Models.Validation.Partner
{
    public class GetNearPartnersByCoordinatesModelValidation : AbstractValidator<GetNearPartnersByCoordinatesRequest>
    {
        public GetNearPartnersByCoordinatesModelValidation()
        {
            RuleFor(p => p.Latitude)
                .InclusiveBetween(-90, 90)
                .WithMessage("Latitude value must be between -90 and 90.");

            RuleFor(p => p.Longitude)
                .InclusiveBetween(-180, 180)
                .WithMessage("Longitude value must be between -180 and 180.");

            RuleFor(p => p.RadiusInKm)
                .InclusiveBetween(1, 100)
                .WithMessage("RadiusInKm value must be between 1 and 100.");
        }
    }
}
