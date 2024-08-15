using ApplicationCore.UseCases.Employees.CreateEmployee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Validations
{
    public class CreateEmployeesRequestValidators : AbstractValidator<CreateEmployeesRequest>
    {
        public CreateEmployeesRequestValidators()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty()
                .WithMessage("Name is required")
                .Length(3, 50).WithMessage("{PropertyName} is between 3 to 50");
            RuleFor(x => x.Adress).Length(5, 250);
               
            RuleFor(x => x.ContactNo).NotNull().NotEmpty()
                .Length(7 , 11)
                .Matches(@"^\d+$")
                .WithMessage("{PropertyName} is must be in between 7 to 11 digits");

            RuleFor(x => x.Email).NotNull().NotEmpty()
                .WithMessage("{PropertyName} is an invalid emial");

            RuleFor(x => x.Designation).NotNull().NotEmpty()
                .Length(2, 14).WithMessage("{PropertyName} is between 2 to 14");

        }
    }
}
