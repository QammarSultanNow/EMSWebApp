using ApplicationCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Assets.GetAssestById
{
    public class GetAssestByIdHandler : IRequestHandler<GetAssestByIdRequest, AssetsModel.Assets>
    {
        private readonly IAssetsRepository _assetsRepository;
        public GetAssestByIdHandler(IAssetsRepository assetsRepository)
        {
            _assetsRepository = assetsRepository;
        }
        public async Task<AssetsModel.Assets> Handle(GetAssestByIdRequest request, CancellationToken cancellationToken)
        {
           var result = await _assetsRepository.GetAssetsRecordById(request.Id);
            return result;
        }
    }
}
