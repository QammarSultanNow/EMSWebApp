using ApplicationCore.AssetsModel;
using ApplicationCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Assets.AddAssignedAsset
{
    public class AddAssignedAssetHandler : IRequestHandler<AddAssignedAssetRequest, int>
    {
        private readonly IAssetsRepository _assetsRepository;
        public AddAssignedAssetHandler(IAssetsRepository assetsRepository)
        {
            _assetsRepository = assetsRepository;
        }
        public async Task<int> Handle(AddAssignedAssetRequest request, CancellationToken cancellationToken)
        {
            EmployeeAssets employeeAsset = new EmployeeAssets();
            employeeAsset.AssetId = request.AssetId;    
            employeeAsset.EmployeeId   = request.EmployeeId;    
            employeeAsset.CreatedAt = request.CreatedAt;    
            employeeAsset.CreatedBy = request.CreatedBy;    
            employeeAsset.ModifiedAt = request.ModifiedAt;    
            employeeAsset.ModifiedBy = request.ModifiedBy;    
            employeeAsset.Remarks = request.Remarks;

            var result = await _assetsRepository.AssignEmployeeAsset(employeeAsset);
            return result;
            
        }
    }
}
