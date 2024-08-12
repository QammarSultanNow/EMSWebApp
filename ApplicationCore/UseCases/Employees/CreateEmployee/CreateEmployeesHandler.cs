using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Employees.CreateEmployee
{
    public class CreateEmployeesHandler : IRequestHandler<CreateEmployeesRequest, int>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private IHttpContextAccessor _httpContextAccessor;

        private readonly IUploadImageService _image;

        public CreateEmployeesHandler(IEmployeeRepository employeeRepository, IMapper mapper, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor, IUploadImageService image)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _image = image;
        }
        public async Task<int> Handle(CreateEmployeesRequest request, CancellationToken cancellationToken)
        {
            await _image.UploadImageByUser(request);

            var user = _httpContextAccessor.HttpContext?.User;
            request.CreatedBy = _userManager.GetUserId(user);

            var employee =  _mapper.Map<EmployeeInformation>(request);

            var result = await _employeeRepository.AddEmplyee(employee);
            return request.Id;
        }
    }
}
