using FluentValidation;
using Library.Models.RequestModels;

namespace Library.Validators
{
    public class BorrowBookRequestValidator : AbstractValidator<BorrowBookRequest>
    {
        public BorrowBookRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.BookId)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .NotNull()
                .WithMessage("{PropertyName} cannot be null.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
            RuleFor(x => x.EmployeeId)
           .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .NotNull()
                .WithMessage("{PropertyName} cannot be null.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
            RuleFor(x => x.date)
                .Must(BeAValidDate)
                .WithMessage("{PropertyName} is not a valid date");
        }
        private bool BeAValidDate(DateTime? date)
        {
            return date == null || date >= DateTime.Now;
        }
    }
}
