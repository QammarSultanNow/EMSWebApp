using ApplicationCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.AssetsModel;

namespace ApplicationCore.UseCases.Assets.CreateAssets
{
    public class CreateAssetsHandler : IRequestHandler<CreateAssetsRequst, int>
    {
        private readonly IAssetsRepository _assetsRepository;
        public CreateAssetsHandler(IAssetsRepository assetsRepository)
        {
            _assetsRepository = assetsRepository;
        }
        public async Task<int> Handle(CreateAssetsRequst request, CancellationToken cancellationToken)
        {
            ApplicationCore.AssetsModel.Assets  asset  = new ApplicationCore.AssetsModel.Assets();
            asset.Name = request.Name;
            asset.Description = request.Description;
            asset.PurchasingPrice = request.PurchasingPrice;
            asset.Status = request.Status;
            asset.ImagePath = request.ImagePath;
            asset.CreatedAt = request.CreatedAt;
            asset.CreatedBy = request.CreatedBy;
            asset.ModifiedAt = request.ModifiedAt;
            asset.ModifiedBy = request.ModifiedBy;


           var result = await _assetsRepository.AddAssetsAsync(asset);
            return result;
        }
    }
}
