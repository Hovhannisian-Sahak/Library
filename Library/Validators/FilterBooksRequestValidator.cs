using FluentValidation;
using Library.Models.RequestModels;

namespace Library.Validators
{
    public class FilterBooksRequestValidator : AbstractValidator<FilterBooksRequest>
    {
        public FilterBooksRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.SearchText)
                .MaximumLength(50)
                .WithMessage("{PropertyName} must be maximum 40 characters long");
        }
    }
}
