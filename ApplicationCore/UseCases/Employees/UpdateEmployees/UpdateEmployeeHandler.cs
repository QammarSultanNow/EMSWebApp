using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using AutoMapper;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Employees.UpdateEmployees
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeRequest, int>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private IHttpContextAccessor _httpContextAccessor;

        private readonly IUploadImageService _image;
        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository, IMapper mapper, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor, IUploadImageService image)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _image = image;
        }
        public async Task<int> Handle(UpdateEmployeeRequest request, CancellationToken cancellationToken)
        {

            await _image.UpdateImageByUser(request);

            var user = _httpContextAccessor.HttpContext?.User;
            request.ModifiedBy = _userManager.GetUserId(user);
            request.ModifiedOn = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            var employee = _mapper.Map<EmployeeInformation>(request);

            var result = await _employeeRepository.UpdateEmplyeesRecord(employee, request.Id);
            return request.Id;
        }
    }
}
