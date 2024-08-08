using ApplicationCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Assets.UnassignAssets
{
    public class UnassignAssetHandler : IRequestHandler<UnassignAssetRequest, int>
    {
        private readonly IAssetsRepository _assetsRepository;
        public UnassignAssetHandler(IAssetsRepository assetsRepository)
        {
            _assetsRepository = assetsRepository;
        }
        public async Task<int> Handle(UnassignAssetRequest request, CancellationToken cancellationToken)
        {
            var result = await _assetsRepository.UnassignedAssetRepo(request.Id);
            return result;  
        }
    }
}
