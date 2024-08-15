using ApplicationCore.UseCases.Departments.CreateDepartment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Validations
{
    public class CreateDepartmentRequestValidators : AbstractValidator<CreateDepartmentRequest>
    {
        public CreateDepartmentRequestValidators()
        {
                RuleFor(x => x.DepartmentName)
                .NotEmpty().NotNull()
                .Length(2 , 50).WithMessage("{PropertyName} must be between 2 to 50 letters");
        }
    }
}
