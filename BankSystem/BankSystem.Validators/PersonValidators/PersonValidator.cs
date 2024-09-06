using BankSystemDTOs.PersonDTOs;
using FluentValidation;

namespace BankSystem.Validators.PersonValidators
{
    public class PersonValidator : AbstractValidator<CreateOrUpdatePersonDto>
    {
        public PersonValidator()
        {
            RuleFor(person => person.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(100).WithMessage("First name can't be longer than 100 characters.");

            RuleFor(person => person.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100).WithMessage("Last name can't be longer than 100 characters.");

            RuleFor(person => person.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^(\+?0*[1-9]\d{0,14}|\d{10,15})$").WithMessage("Invalid phone number format."); // Example regex for international phone numbers
        }
    }
}
