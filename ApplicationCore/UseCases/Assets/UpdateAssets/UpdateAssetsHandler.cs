using ApplicationCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Assets.UpdateAssets
{
    public class UpdateAssetsHandler : IRequestHandler<UpdateAssetsRequest, int>
    {
        private readonly IAssetsRepository _assetsRepository;
        public UpdateAssetsHandler(IAssetsRepository assetsRepository)
        {
            _assetsRepository = assetsRepository;
        }
        public async Task<int> Handle(UpdateAssetsRequest request, CancellationToken cancellationToken)
        {
            ApplicationCore.AssetsModel.Assets asset = new ApplicationCore.AssetsModel.Assets();
            asset.Id = request.Id;
            asset.Name = request.Name;
            asset.Description = request.Description;
            asset.PurchasingPrice = request.PurchasingPrice;
            asset.Status = request.Status;
            asset.ImagePath = request.ImagePath;
            asset.CreatedAt = request.CreatedAt;
            asset.CreatedBy = request.CreatedBy;
            asset.ModifiedAt = request.ModifiedAt;
            asset.ModifiedBy = request.ModifiedBy;


           var result = await _assetsRepository.UpdateAssetsRecords(asset);
            return result;
        }
    }
}
