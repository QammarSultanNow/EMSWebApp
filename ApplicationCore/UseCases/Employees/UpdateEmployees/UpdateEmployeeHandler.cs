using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Azure.Core;
using MediatR;
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
        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<int> Handle(UpdateEmployeeRequest request, CancellationToken cancellationToken)
        {
            EmployeeInformation employee = new EmployeeInformation();

            employee.Name = request.Name;
            employee.Email = request.Email;
            employee.Adress = request.Adress;
            employee.Designation = request.Designation;
            employee.ContactNo = request.ContactNo;
            employee.ImagePath = request.ImagePath;
            employee.CreatedOn = request.CreatedOn;
            employee.CreatedBy = request.CreatedBy;
            employee.ModifiedOn = request.ModifiedOn;
            employee.ModifiedBy = request.ModifiedBy;
            employee.DepartmentId = request.DepartmentId;

            var result = await _employeeRepository.UpdateEmplyeesRecord(employee, request.Id);
            return request.Id;
        }
    }
}
