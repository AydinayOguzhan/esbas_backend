using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CourseValidator: AbstractValidator<Course>
    {
        public CourseValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().MinimumLength(3);
            RuleFor(c => c.Description).NotNull().NotEmpty().MinimumLength(3);
        }
    }
}
