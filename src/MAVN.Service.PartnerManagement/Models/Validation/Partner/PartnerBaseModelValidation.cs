using FluentValidation;
using MAVN.Service.PartnerManagement.Client.Models.Partner;

namespace MAVN.Service.PartnerManagement.Models.Validation.Partner
{
    public class PartnerBaseModelValidation<T> : AbstractValidator<T>
        where T : PartnerBaseModel
    {
        public PartnerBaseModelValidation()
        {
            //RuleFor(p => p.Name)
            //    .NotNull()
            //    .NotEmpty()
            //    .MinimumLength(3)
            //    .MaximumLength(50)
            //    .WithMessage("The partner name should be present and within a range of 3 to 50 characters long.");

            RuleFor(p => p.Description)
                .MinimumLength(3)
                .MaximumLength(1000)
                .WithMessage("The description can be empty or within a range of 3 to 1000 characters long.");

            RuleFor(p => p.AmountInTokens)
                .Must((model, value) => model.UseGlobalCurrencyRate || !model.UseGlobalCurrencyRate && value > 0)
                .WithMessage("Amount in tokens should be greater than 0.");

            RuleFor(p => p.AmountInCurrency)
                .Must((model, value) => model.UseGlobalCurrencyRate || !model.UseGlobalCurrencyRate && value > 0)
                .WithMessage("Amount in currency should be greater than 0.");

            RuleFor(p => p.BusinessVertical)
                .NotNull()
                .NotEmpty()
                .WithMessage("The Business Vertical should be present");
        }
    }
}
