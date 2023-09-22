using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class RegisterValidator: AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(u => u.Email).EmailAddress().NotNull().NotEmpty();
            RuleFor(u => u.FirstName).NotNull().NotEmpty().MinimumLength(2);
            RuleFor(u => u.LastName).NotNull().NotEmpty().MinimumLength(2);
            RuleFor(u => u.Password).NotNull().NotEmpty().MinimumLength(6);
        }
    }
}
