using System;
using FluentValidation;
using MAVN.Service.PartnerManagement.Client.Models.Partner;

namespace MAVN.Service.PartnerManagement.Models.Validation.Partner
{
    public class PartnerListRequestModelValidation : AbstractValidator<PartnerListRequestModel>
    {
        public PartnerListRequestModelValidation()
        {
            RuleFor(p => p.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(500)
                .WithMessage($"The Page Size should be within a range of 1 to 500.");

            RuleFor(p => p.CurrentPage)
                .GreaterThan(0)
                .LessThanOrEqualTo(10_000)
                .WithMessage($"The Current Page should be within a range of 1 to 10000.");
        }
    }
}
