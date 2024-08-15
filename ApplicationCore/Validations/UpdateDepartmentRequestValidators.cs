using ApplicationCore.UseCases.Departments.UpdateDepartment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Validations
{
    public class UpdateDepartmentRequestValidators : AbstractValidator<UpdateDepartmentRequest>
    {
        public UpdateDepartmentRequestValidators()
        {
                RuleFor(x => x.DepartmentName).NotNull().NotEmpty()
                .Length(2,50).WithMessage("{PropertyName} must be between 2 to 50");
        }
    }
}
