using FluentValidation;
using Library.Models.RequestModels;
using System.Text.RegularExpressions;

namespace Library.Validators
{
    public class StudentLoginRequestValidator : AbstractValidator<StudentLoginRequest>
    {
        public StudentLoginRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
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
                .Must(BeAValidEmail)
                .WithMessage("Invalid email format.")
                .MinimumLength(11)
                .WithMessage("{PropertyName} must be at least 11 characters long")
                .MaximumLength(35)
                .WithMessage("{PropertyName} must be maximum 35 characters long");
        }
        private bool BeAValidEmail(string email)
        {
            const string emailPattern = @"^[\w\.-]+@[\w\.-]+\.\w+$";
            return !string.IsNullOrWhiteSpace(email) && Regex.IsMatch(email, emailPattern);
        }
    }
}
