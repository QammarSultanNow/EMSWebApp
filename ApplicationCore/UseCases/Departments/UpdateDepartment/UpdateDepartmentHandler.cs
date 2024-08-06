using ApplicationCore.Models;
using EMSWebApp.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Departments.UpdateDepartment
{
    
    public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentRequest, int>
    {
        private readonly IDepartmentRepository _departmentRepository;
        public UpdateDepartmentHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<int> Handle(UpdateDepartmentRequest request, CancellationToken cancellationToken)
        {
            Department dpt = new Department();
            dpt.DepartmentName = request.DepartmentName;

           var result = await _departmentRepository.UpdateDepartment(dpt, request.Id);
            return result;
        }
    }
}
