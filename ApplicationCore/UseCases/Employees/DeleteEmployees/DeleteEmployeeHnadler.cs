using ApplicationCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Employees.DeleteEmployees
{
    public class DeleteEmployeeHnadler : IRequestHandler<DeleteEmployeesRequest, int>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public DeleteEmployeeHnadler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<int> Handle(DeleteEmployeesRequest request, CancellationToken cancellationToken)
        {
            await _employeeRepository.DeleteEmployeesRecord(request.Id);
            return request.Id;
        }
    }

}
