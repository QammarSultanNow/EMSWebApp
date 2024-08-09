using ApplicationCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.AssetsModel;
using AutoMapper;

namespace ApplicationCore.UseCases.Assets.CreateAssets
{
    public class CreateAssetsHandler : IRequestHandler<CreateAssetsRequst, int>
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IMapper _mapper;
        public CreateAssetsHandler(IAssetsRepository assetsRepository, IMapper mapper)
        {
            _assetsRepository = assetsRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateAssetsRequst request, CancellationToken cancellationToken)
        {
          var asset =  _mapper.Map<AssetsModel.Assets>(request);

           var result = await _assetsRepository.AddAssetsAsync(asset);
            return result;
        }
    }
}
