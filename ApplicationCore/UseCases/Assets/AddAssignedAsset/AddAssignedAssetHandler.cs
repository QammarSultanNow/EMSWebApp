using ApplicationCore.AssetsModel;
using ApplicationCore.Interfaces;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public AddAssignedAssetHandler(IAssetsRepository assetsRepository, IMapper mapper)
        {
            _assetsRepository = assetsRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(AddAssignedAssetRequest request, CancellationToken cancellationToken)
        {
           var employeeAsset =  _mapper.Map<EmployeeAssets>(request);

            var result = await _assetsRepository.AssignEmployeeAsset(employeeAsset);
            return result;
            
        }
    }
}
