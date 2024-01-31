using Domain.DTO;
using FluentValidation;

namespace Application.Validators
{
    public class InsuranceValidator : AbstractValidator<InsuranceDTO>
    {
        public InsuranceValidator()
        {
            RuleFor(x => x.Title)
                .NotNull().WithMessage("Insurance title is required field")
                .NotEmpty().WithMessage("Insurance title field can`t be an empty string");

            RuleFor(x => x.Description)
                .NotNull().WithMessage("Insurance description is required field")
                .NotEmpty().WithMessage("Insurance description field can`t be an empty string");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Insurance price is required field")
                .Must(MoreThenZero).WithMessage("Invalid integer range");
        }

        private bool MoreThenZero(int value)
        {
            const int minValue = 0;
            return value > minValue;
        }
    }
}
