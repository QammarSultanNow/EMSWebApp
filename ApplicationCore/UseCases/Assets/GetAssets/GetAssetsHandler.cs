using ApplicationCore.AssetsModel;
using ApplicationCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Assets.GetAssets
{
    public class GetAssetsHandler : IRequestHandler<GetAssetsRequest, IEnumerable<ListEmployeeAssetViewModel>>
    {
        private readonly IAssetsRepository _assetsRepository;
        public GetAssetsHandler(IAssetsRepository assetsRepository)
        {
            _assetsRepository = assetsRepository;
        }
        public async Task<IEnumerable<ListEmployeeAssetViewModel>> Handle(GetAssetsRequest request, CancellationToken cancellationToken)
        {
           var result = await _assetsRepository.GetAssetRecords();
            return result;  
        }
    }
}
