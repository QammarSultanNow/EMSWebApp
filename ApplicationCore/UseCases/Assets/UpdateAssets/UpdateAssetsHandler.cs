using ApplicationCore.AssetsModel;
using ApplicationCore.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IUploadImageService _uploadImageService;
        public UpdateAssetsHandler(IAssetsRepository assetsRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, IUploadImageService uploadImageService)
        {
            _assetsRepository = assetsRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _uploadImageService = uploadImageService;
        }
        public async Task<int> Handle(UpdateAssetsRequest request, CancellationToken cancellationToken)
        {
            await _uploadImageService.UpdateAssetImage(request);

            var user = _httpContextAccessor.HttpContext?.User;
            request.ModifiedBy = _userManager.GetUserId(user);

            var asset =   _mapper.Map<AssetsModel.Assets>(request);
            var result = await _assetsRepository.UpdateAssetsRecords(asset);
            return result;
        }
    }
}
