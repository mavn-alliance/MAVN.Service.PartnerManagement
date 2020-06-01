using System.Text.RegularExpressions;
using FluentValidation;
using MAVN.Service.PartnerManagement.Client.Models;
using MAVN.Service.PartnerManagement.Client.Models.Partner;

namespace MAVN.Service.PartnerManagement.Models.Validation.ContactPerson
{
    public class ContactPersonModelValidation : AbstractValidator<ContactPersonModel>
    {
        private readonly Regex _onlyLettersRegex = new Regex(@"^((?![1-9!@#$%^&*()_+{}|:\""?></,;[\]\\=~]).)+$");
        public ContactPersonModelValidation()
        {
            RuleFor(p => p.FirstName)
                .NotNull()
                .NotEmpty()
                .When(x => !string.IsNullOrEmpty(x.Email) || !string.IsNullOrEmpty(x.LastName) || !string.IsNullOrEmpty(x.PhoneNumber))
                .MinimumLength(2)
                .MaximumLength(50)
                .WithMessage("The First Name should be present and within a range of 2 to 50 characters long.")
                .Must(o => o != null && _onlyLettersRegex.IsMatch(o))
                .WithMessage("First Name should contains only letters");

            RuleFor(p => p.LastName)
                .NotNull()
                .NotEmpty()
                .When(x => !string.IsNullOrEmpty(x.Email) || !string.IsNullOrEmpty(x.FirstName) || !string.IsNullOrEmpty(x.PhoneNumber))
                .MinimumLength(2)
                .MaximumLength(50)
                .WithMessage("The Last Name should be present and within a range of 2 to 50 characters long.")
                .Must(o => o != null && _onlyLettersRegex.IsMatch(o))
                .WithMessage("Last Name should contains only letters");

            RuleFor(p => p.Email)
                .NotNull()
                .NotEmpty()
                .When(x => !string.IsNullOrEmpty(x.FirstName) || !string.IsNullOrEmpty(x.LastName) || !string.IsNullOrEmpty(x.PhoneNumber))
                .EmailAddress()
                .WithMessage("The Email should be present and within a valid email address.");

            RuleFor(p => p.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .When(x => !string.IsNullOrEmpty(x.Email) || !string.IsNullOrEmpty(x.FirstName) || !string.IsNullOrEmpty(x.LastName))
                .MinimumLength(3)
                .MaximumLength(30)
                .WithMessage("The Phone number should be present and within a range of 3 to 30 characters long.");
        }
    }
}
