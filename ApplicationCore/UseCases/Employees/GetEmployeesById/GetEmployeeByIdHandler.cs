using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Employees.GetEmployeesById
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeesByIdRequest, EmployeeInformation>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public GetEmployeeByIdHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<EmployeeInformation> Handle(GetEmployeesByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _employeeRepository.GetAllEmployeeById(request.Id);
            return result;
        }
    }
}