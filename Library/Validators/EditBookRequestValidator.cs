using Library.Models.RequestModels;
using FluentValidation;
namespace Library.Validators
{
    public class EditBookRequestValidator : AbstractValidator<EditBookRequest>
    {
        public EditBookRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Id)
              .NotEmpty()
              .WithMessage("{PropertyName} cannot be empty.")
              .NotNull()
              .WithMessage("{PropertyName} cannot be null.")
              .GreaterThan(0)
              .WithMessage("{PropertyName} must be greater than 0");
            RuleFor(x => x.Name)
                .MinimumLength(4)
                .WithMessage("{PropertyName} must be at least 4 characters long")
                .MaximumLength(40)
                .WithMessage("{PropertyName} must be maximum 40 characters long");
            RuleFor(x => x.Author)
                .MinimumLength(2)
                .WithMessage("{PropertyName} must be at least 4 characters long")
                .MaximumLength(40)
                .WithMessage("{PropertyName} must be maximum 40 characters long");
            RuleFor(x => x.Count)
              .GreaterThan(0)
              .WithMessage("BookId must be greater than 0");
        }
    }
}
