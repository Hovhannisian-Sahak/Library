using FluentValidation;
using Library.Models.RequestModels;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Library.Validators
{
    public class FilterEmployeesRequestValidator :AbstractValidator<FilterEmployeesRequest>
    {
        public FilterEmployeesRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.SearchText)
                .MaximumLength(50)
                .WithMessage("{PropertyName} must be maximum 50 characters long");
            RuleFor(x => x.ProfessionId)
                .InclusiveBetween(1, 3)
                .WithMessage("{PropertyName} must be between 1 and 3");
            RuleFor(x => x.StartDate)
               .Must(BeAValidDate)
               .WithMessage("{PropertyName} is not a valid date");
            RuleFor(x => x.EndDate)
               .Must(BeAValidDate)
               .WithMessage("{PropertyName} is not a valid date");
        }
        private bool BeAValidDate(DateTime? date)
        {
            return date == null || date <= DateTime.Now;
        }


    }
}
