using ApplicationCore.AssetsModel;
using ApplicationCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Assets.ListAssignedAsset
{
    public class ListAssignedAssetHandler : IRequestHandler<ListAssignedAssetRequest, IEnumerable<ListEmployeeAssetViewModel>>
    {
        private readonly IAssetsRepository _assetsRepository;
        public ListAssignedAssetHandler(IAssetsRepository assetsRepository)
        {
            _assetsRepository = assetsRepository;
        }
        public async Task<IEnumerable<ListEmployeeAssetViewModel>> Handle(ListAssignedAssetRequest request, CancellationToken cancellationToken)
        {
           var result = await _assetsRepository.ListAssetandEmployee(request.Id);
            return result;
        }
    }
}
