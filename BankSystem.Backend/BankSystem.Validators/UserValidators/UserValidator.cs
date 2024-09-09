using BankSystem.DTOs.UserDTOs;
using BankSystem.Validators.PersonValidators;
using FluentValidation;

namespace BankSystem.Validators.UserValidators
{
    public class UserValidator : AbstractValidator<CreateOrUpdateUserDto>
    {
        public UserValidator()
        {
            // Validate Username
            RuleFor(user => user.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(100).WithMessage("Username can't be longer than 100 characters.");

            // Validate Password
            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(255).WithMessage("Password can't be longer than 255 characters.");

            // Validate IsActive
            RuleFor(user => user.IsActive)
                .Must(value => value == true || value == false)
                .WithMessage("IsActive must be a boolean value.");

            // Use the existing PersonValidator to validate the Person property
            RuleFor(user => user.Person)
                .SetValidator(new PersonValidator());
        }
    }
}
