using FluentValidation;
using Library.Models.RequestModels;
using System.Text.RegularExpressions;

namespace Library.Validators
{
    public class StudentSignupRequestValidator : AbstractValidator<StudentSignupRequest>
    {
        public StudentSignupRequestValidator()
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
            RuleFor(x => x.MiddleName)
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
                .Must(BeAValidEmail)
                .WithMessage("Invalid email format.")
                .MinimumLength(11)
                .WithMessage("{PropertyName} must be at least 11 characters long")
                .MaximumLength(35)
                .WithMessage("{PropertyName} must be maximum 35 characters long");
            RuleFor(x => x.AcademicYear)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
                .NotNull()
                .WithMessage("{PropertyName} cannot be null.")
                .Must(BeValidDateRange)
                .WithMessage("Invalid date range format (YYYY-YYYY).");
            RuleFor(x => x.FacultyId)
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
        private bool BeValidDateRange(string dateRange)
        {
            if (!string.IsNullOrWhiteSpace(dateRange))
            {
                string[] parts = dateRange.Split('-');
                if (parts.Length == 2 && parts[0].Length == 4 && parts[1].Length == 4)
                {
                    int startYear, endYear;
                    if (int.TryParse(parts[0], out startYear) && int.TryParse(parts[1], out endYear))
                    {
                        return startYear <= endYear;
                    }
                }
            }
            return false;
        }
    }
}
