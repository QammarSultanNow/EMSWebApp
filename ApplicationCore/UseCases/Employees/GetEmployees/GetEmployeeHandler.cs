using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using MediatR;
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
        public GetEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<IEnumerable<EmployeeViewModel>> Handle(GetEmployeeRequest request, CancellationToken cancellationToken)
        {
           var result = await _employeeRepository.GetAllEmployee(request.UserID, request.Id);
            return result;
        }
    }
}
