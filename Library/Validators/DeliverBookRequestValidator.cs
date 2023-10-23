using FluentValidation;
using Library.Models.RequestModels;

namespace Library.Validators
{
    public class DeliverBookRequestValidator : AbstractValidator<DeliverBookRequest>
    {
        public DeliverBookRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.BookId)
            .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .NotNull()
                .WithMessage("{PropertyName} cannot be null.")
                .GreaterThan(0).WithMessage("BookId must be greater than 0");
            RuleFor(x => x.StudentId)
           .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .NotNull()
                .WithMessage("{PropertyName} cannot be null.")
                .GreaterThan(0).WithMessage("BookId must be greater than 0");
        }
            
    }
}
