using ApplicationCore.AssetsModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Assets.AssignAsset
{
    public class AssignAssetRequest : IRequest<EmployeeAssestViewModel>
    {
        public int? Id { get; set; }
    }
}
