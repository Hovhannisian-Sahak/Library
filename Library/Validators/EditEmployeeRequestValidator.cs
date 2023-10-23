using FluentValidation;
using Library.Models.RequestModels;
using System.Text.RegularExpressions;

namespace Library.Validators
{
    public class EditEmployeeRequestValidator : AbstractValidator<EditEmployeeRequest>
    {
        public EditEmployeeRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.FirstName)
                .MinimumLength(2)
                .WithMessage("{PropertyName} must be at least 2 characters long")
                .MaximumLength(20)
                .WithMessage("{PropertyName} must be maximum 20 characters long");
            RuleFor(x => x.LastName)
                .MinimumLength(2)
                .WithMessage("{PropertyName} must be at least 2 characters long")
                .MaximumLength(20)
                .WithMessage("{PropertyName} must be maximum 20 characters long");
            RuleFor(x => x.Salary)
                .InclusiveBetween(120000, 250000)
                .WithMessage("{PropertyName} must be between 120000 and 250000");
            RuleFor(x => x.ProfessionId)
                .InclusiveBetween(1, 3)
                .WithMessage("{PropertyName} must be between 1 and 3");
        }
    }
}
