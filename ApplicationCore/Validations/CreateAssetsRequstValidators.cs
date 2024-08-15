using ApplicationCore.UseCases.Assets.CreateAssets;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Validations
{
    public class CreateAssetsRequstValidators : AbstractValidator<CreateAssetsRequst>
    {
        public CreateAssetsRequstValidators()
        {
                RuleFor(x => x.Name)
                .NotEmpty().NotNull()
                .Length(2 , 100)
                .WithMessage("{PropertyName} must be between 2 to 100 letters");

            RuleFor(x => x.Description)
                .NotEmpty().NotNull()
                .Length(2, 100)
                .WithMessage("{PropertyName} must be between 2 to 100 letters");

            RuleFor(x => x.PurchasingPrice)
                .NotEmpty().NotNull()
                 .GreaterThan(0)
                 .LessThan(100000).WithMessage("PropertyName must be less 1Lac and greater than 0");



        }
    }
}
