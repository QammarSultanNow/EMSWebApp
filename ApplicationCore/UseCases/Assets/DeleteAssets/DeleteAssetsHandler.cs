using ApplicationCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Assets.DeleteAssets
{
    public class DeleteAssetsHandler : IRequestHandler<DeleteAssetsRequest, int>
    {
        private readonly IAssetsRepository _assetsRepository;
        public DeleteAssetsHandler(IAssetsRepository assetsRepository)
        {
            _assetsRepository = assetsRepository;
        }
        public async Task<int> Handle(DeleteAssetsRequest request, CancellationToken cancellationToken)
        {
            var result = await _assetsRepository.DeleteAssetsRecords(request.Id);
          return result;
        }
    }
}
