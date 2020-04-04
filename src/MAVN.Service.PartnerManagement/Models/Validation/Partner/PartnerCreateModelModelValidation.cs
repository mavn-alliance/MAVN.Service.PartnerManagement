using System;
using System.Linq;
using FluentValidation;
using MAVN.Service.PartnerManagement.Client.Models.Partner;
using MAVN.Service.PartnerManagement.Models.Validation.Location;

namespace MAVN.Service.PartnerManagement.Models.Validation.Partner
{
    public class PartnerCreateModelModelValidation : PartnerBaseModelValidation<PartnerCreateModel>
    {
        public PartnerCreateModelModelValidation() : base()
        {
            RuleFor(p => p.ClientId)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(64)
                .WithMessage("The Client Id should be present and within range of 6 to 64 characters long.");

            RuleFor(p => p.ClientSecret)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(64)
                .WithMessage("The Client Secret should be present and within range of 6 to 64 characters long.");

            RuleFor(p => p.CreatedBy)
                .Must(p => p != Guid.Empty)
                .WithMessage("The Created By should be valid non-empty Guid.");

            RuleFor(p => p.Locations)
                .Must(p => p != null && p.Count > 0)
                .WithMessage("The Partner should have at least one location.")
                .Must(l => l == null
                           || l.GroupBy(g => g.ExternalId).All(x => x.Count() == 1))
                .WithMessage("Partner should have only locations with unique external ids."); 

            RuleForEach(p => p.Locations)
                .SetValidator(new LocationCreateModelValidation());
        }
    }
}
