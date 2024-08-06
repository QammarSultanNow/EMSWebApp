using ApplicationCore.Models;
using EMSWebApp.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Departments.GetDepartment
{
    public class GetDepartmentHandler : IRequestHandler<GetDepartmentRequest, IEnumerable<DepartmentEmployeeCount>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        public GetDepartmentHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<DepartmentEmployeeCount>> Handle(GetDepartmentRequest request, CancellationToken cancellationToken)
        {
            var result = await _departmentRepository.EmployeeCount();
            return result;
        }



        
    }
}
