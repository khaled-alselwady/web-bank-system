using BankSystem.DTOs.ClientDTOs;
using BankSystem.Validators.PersonValidators;
using FluentValidation;

namespace BankSystem.Validators.ClientValidators
{
    public class ClientValidator : AbstractValidator<CreateOrUpdateClientDto>
    {
        public ClientValidator()
        {
            // Adding rules specific to CreateOrUpdateClientDto
            RuleFor(client => client.AccountNumber)
                .NotEmpty().WithMessage("Account number is required.")
                .MaximumLength(10).WithMessage("Account number can't be longer than 4 characters.");

            RuleFor(client => client.PinCode)
                .NotEmpty().WithMessage("Pin code is required.")
                .Length(4).WithMessage("Pin code must be 4 characters long.");

            RuleFor(client => client.Balance)
                .GreaterThanOrEqualTo(0).WithMessage("Balance cannot be negative.");

            RuleFor(client => client.IsActive)
                .Must(value => value == true || value == false)
                .WithMessage("IsActive must be a boolean value.");

            // Use the existing PersonValidator to validate the Person property
            RuleFor(client => client.PersonDto)
                .SetValidator(new PersonValidator());
        }
    }
}
