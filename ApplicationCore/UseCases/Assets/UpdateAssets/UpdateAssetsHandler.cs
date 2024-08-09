using ApplicationCore.Interfaces;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public UpdateAssetsHandler(IAssetsRepository assetsRepository, IMapper mapper)
        {
            _assetsRepository = assetsRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateAssetsRequest request, CancellationToken cancellationToken)
        {
          var asset =   _mapper.Map<AssetsModel.Assets>(request);

           var result = await _assetsRepository.UpdateAssetsRecords(asset);
            return result;
        }
    }
}
