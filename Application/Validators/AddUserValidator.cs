using Domain.DTO;
using FluentValidation;

namespace Application.Validators
{
    public class AddUserValidator : AbstractValidator<UserDTO>
    {
        public AddUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull().WithMessage("User firstname is required field")
                .NotEmpty().WithMessage("User firstnam field can`t be an empty string");

            RuleFor(x => x.LastName)
                .NotNull().WithMessage("User lastname is required field")
                .NotEmpty().WithMessage("User lastname field can`t be an empty string");

            RuleFor(x => x.Email)
                .NotNull().WithMessage("User email is required field")
                .NotEmpty().WithMessage("User email field can`t be an empty string");
        }
    }
}
