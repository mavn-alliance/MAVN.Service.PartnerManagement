using System;
using System.Linq;
using FluentValidation;
using MAVN.Service.PartnerManagement.Client.Models.Partner;
using MAVN.Service.PartnerManagement.Models.Validation.Location;

namespace MAVN.Service.PartnerManagement.Models.Validation.Partner
{
    public class PartnerUpdateModelValidation: PartnerBaseModelValidation<PartnerUpdateModel>
    {
        public PartnerUpdateModelValidation(): base()
        {
            RuleFor(p => p.Id)
                .Must(p => p != Guid.Empty)
                .WithMessage("The Id should be valid non-empty Guid.");

            RuleFor(p => p.ClientSecret)
                .MinimumLength(6)
                .MaximumLength(64)
                .WithMessage("The Client Secret should be empty or within range of 6 to 64 characters long.");

            RuleFor(p => p.ClientId)
                .MinimumLength(6)
                .MaximumLength(64)
                .WithMessage("The Client Id should be empty or within a range of 6 to 64 characters long.")
                .Must((p, c) => p != null && (!string.IsNullOrEmpty(p.ClientId) || string.IsNullOrEmpty(p.ClientSecret)))
                .WithMessage("When changing the client id, a client secret is required.");

            RuleFor(p => p.Locations)
                //.Must(p => p != null && p.Count > 0)
                //.WithMessage("The Partner should have at least one location.")
                .Must(l => l == null
                           || l.GroupBy(g => g.ExternalId).All(x => x.Count() == 1))
                .WithMessage("Partner should have only locations with unique external ids."); ;

            RuleForEach(p => p.Locations)
                .SetValidator(new LocationUpdateModelValidation());
        }
    }
}
