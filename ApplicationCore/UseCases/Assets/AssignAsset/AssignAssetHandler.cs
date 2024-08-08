using ApplicationCore.AssetsModel;
using ApplicationCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Assets.AssignAsset
{
   
    public class AssignAssetHandler : IRequestHandler<AssignAssetRequest, EmployeeAssestViewModel>
    {
        private readonly IAssetsRepository _assetsRepository;
        public AssignAssetHandler(IAssetsRepository assetsRepository)
        {
            _assetsRepository = assetsRepository;
        }
        public async Task<EmployeeAssestViewModel> Handle(AssignAssetRequest request, CancellationToken cancellationToken)
        {
          var result = await  _assetsRepository.EmployeeAssetList(request.Id);
          return result;
        }
    }
}
