using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Employees.GetEmployees
{
    public class GetEmployeeHandler : IRequestHandler<GetEmployeeRequest, IEnumerable<EmployeeViewModel>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private IHttpContextAccessor _httpContextAccessor;
        public GetEmployeeHandler(IEmployeeRepository employeeRepository, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _employeeRepository = employeeRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<EmployeeViewModel>> Handle(GetEmployeeRequest request, CancellationToken cancellationToken)
        {
            var userId = "";
            if (_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
            {
                 userId = "";
            }
            else
            {
                var user = _httpContextAccessor.HttpContext?.User;
                 userId = _userManager.GetUserId(user);
                 request.UserID = userId;
            }
                

            var result = await _employeeRepository.GetAllEmployee(request.UserID, request.Id);
            return result;
        }
    }
}
