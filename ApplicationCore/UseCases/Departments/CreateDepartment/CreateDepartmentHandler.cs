using ApplicationCore.Models;
using EMSWebApp.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Departments.CreateDepartment
{
    public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentRequest, int>
    {
        private readonly IDepartmentRepository _departmentRepository;
        public CreateDepartmentHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<int> Handle(CreateDepartmentRequest request, CancellationToken cancellationToken)
        {
            Department dpt = new Department();
            dpt.DepartmentName = request.DepartmentName;
            var result = await _departmentRepository.AddDepartment(dpt);
            return request.Id;
        }
    }
}
