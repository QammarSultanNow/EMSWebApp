using ApplicationCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.AssetsModel;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.UseCases.Assets.CreateAssets
{
    public class CreateAssetsHandler : IRequestHandler<CreateAssetsRequst, int>
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IUploadImageService _uploadImageService;

        public CreateAssetsHandler(IAssetsRepository assetsRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, IUploadImageService uploadImageService)
        {
            _assetsRepository = assetsRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _uploadImageService = uploadImageService;
        }

        public async Task<int> Handle(CreateAssetsRequst request, CancellationToken cancellationToken)
        {

            await _uploadImageService.UploadAssetImage(request);


            var user = _httpContextAccessor.HttpContext?.User;
            request.CreatedBy = _userManager.GetUserName(user);

            var asset =  _mapper.Map<AssetsModel.Assets>(request);

            var result = await _assetsRepository.AddAssetsAsync(asset);
            return result;
        }
    }
}
