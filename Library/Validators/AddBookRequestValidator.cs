using FluentValidation;
using Library.Models.RequestModels;

namespace Library.Validators
{
    public class AddBookRequestValidator : AbstractValidator<AddBookRequest>
    {
        public AddBookRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .NotNull()
                .WithMessage("{PropertyName} cannot be null.")
                .MinimumLength(4)
                .WithMessage("{PropertyName} must be at least 4 characters long")
                .MaximumLength(40)
                .WithMessage("{PropertyName} must be maximum 40 characters long");
            RuleFor(x => x.Author)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .NotNull()
                .WithMessage("{PropertyName} cannot be null.")
                .MinimumLength(4)
                .WithMessage("{PropertyName} must be at least 4 characters long")
                .MaximumLength(40)
                .WithMessage("{PropertyName} must be maximum 40 characters long");
            RuleFor(x => x.Count)
              .NotEmpty()
              .WithMessage("{PropertyName} is required.")
              .GreaterThan(0)
              .WithMessage("{PropertyName} must be greater than 0");
            RuleFor(x => x.PublishDate)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .Must(BeAValidDate)
              .WithMessage("{PropertyName} is not a valid date");
        }
        private bool BeAValidDate(DateTime PublishDate)
        {
            return PublishDate <= DateTime.Now;
        }
    }
}
