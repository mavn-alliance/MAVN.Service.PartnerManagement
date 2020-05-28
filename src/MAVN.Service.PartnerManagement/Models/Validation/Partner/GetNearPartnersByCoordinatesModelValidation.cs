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
                .WithMessage("Latitude value must be between -90 and 90.")
                .NotNull()
                .When(p => string.IsNullOrEmpty(p.CountryIso3Code) || p.Longitude.HasValue || p.RadiusInKm.HasValue);

            RuleFor(p => p.Longitude)
                .InclusiveBetween(-180, 180)
                .WithMessage("Longitude value must be between -180 and 180.")
                .NotNull()
                .When(p => string.IsNullOrEmpty(p.CountryIso3Code) || p.Latitude.HasValue || p.RadiusInKm.HasValue);

            RuleFor(p => p.RadiusInKm)
                .GreaterThan(0)
                .LessThanOrEqualTo(128)
                .WithMessage("RadiusInKm value must be between 1 and 128.");

            RuleFor(p => p.CountryIso3Code)
                .NotNull()
                .NotEmpty()
                .When(p => !p.Latitude.HasValue && !p.Longitude.HasValue && !p.RadiusInKm.HasValue);
        }
    }
}
