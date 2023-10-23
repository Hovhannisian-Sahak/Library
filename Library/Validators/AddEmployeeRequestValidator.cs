using FluentValidation;
using Library.Models.RequestModels;
using System.Text.RegularExpressions;

namespace Library.Validators
{
    public class AddEmployeeRequestValidator:AbstractValidator<AddEmployeeRequest>
    {
        public AddEmployeeRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .NotNull()
                .WithMessage("{PropertyName} cannot be null.")
                .MinimumLength(2)
                .WithMessage("{PropertyName} must be at least 2 characters long")
                .MaximumLength(20)
                .WithMessage("{PropertyName} must be maximum 20 characters long");
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .NotNull()
                .WithMessage("{PropertyName} cannot be null.")
                .MinimumLength(2)
                .WithMessage("{PropertyName} must be at least 2 characters long")
                .MaximumLength(20)
                .WithMessage("{PropertyName} must be maximum 20 characters long");
            RuleFor(x => x.Password)
               .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .NotNull()
                .WithMessage("{PropertyName} cannot be null.")
                .MinimumLength(8)
                .WithMessage("{PropertyName} must be at least 8 characters long")
                .MaximumLength(20)
                .WithMessage("{PropertyName} must be maximum 20 characters long");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .NotNull()
                .WithMessage("{PropertyName} cannot be null.")
                .WithMessage("{PropertyName} is required.")
                .Must(BeAValidEmail)
                .WithMessage("Invalid email format.")
                .MinimumLength(11)
                .WithMessage("{PropertyName} must be at least 11 characters long")
                .MaximumLength(35)
                .WithMessage("{PropertyName} must be maximum 35 characters long");
            RuleFor(x => x.Salary)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .NotNull()
                .WithMessage("{PropertyName} cannot be null.")
                .InclusiveBetween(120000, 250000)
                .WithMessage("{PropertyName} must be between 120000 and 250000");
            RuleFor(x => x.ProfessionId)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .NotNull()
                .WithMessage("{PropertyName} cannot be null.")
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be higher than 0");
        }
        private bool BeAValidEmail(string email)
        {
            const string emailPattern = @"^[\w\.-]+@[\w\.-]+\.\w+$";
            return !string.IsNullOrWhiteSpace(email) && Regex.IsMatch(email, emailPattern);
        }
    }
}
 