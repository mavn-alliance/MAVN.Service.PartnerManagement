using FluentValidation;
using MAVN.Service.PartnerManagement.Client.Models.Location;
using MAVN.Service.PartnerManagement.Client.Models.Partner;
using MAVN.Service.PartnerManagement.Models.Validation.ContactPerson;

namespace MAVN.Service.PartnerManagement.Models.Validation.Location
{
    public class LocationBaseModelValidation<T> : AbstractValidator<T>
        where T : LocationBaseModel
    {
        public LocationBaseModelValidation()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100)
                .WithMessage("The partner name should be present and within a range of 3 to 100 characters long.");

            RuleFor(p => p.Address)
                .MinimumLength(3)
                .MaximumLength(100)
                .WithMessage("The description can be empty or within a range of 3 to 100 characters long.");

            RuleFor(p => p.ContactPerson)
                .Must(l => l != null)
                .WithMessage("The Contact Person should be present")
                .SetValidator(new ContactPersonModelValidation());

            RuleFor(l=>l.ExternalId)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(80)
                .WithMessage("The external id should be within a range of 1 to 80 characters long.");

            RuleFor(x => x.AccountingIntegrationCode)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(80)
                .WithMessage("The accounting integration code should be within a range of 1 to 80 characters long.");
        }
    }
}
