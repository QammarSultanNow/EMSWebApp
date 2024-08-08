using ApplicationCore.Models;
using EMSWebApp.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Departments.GetDepartmentById
{
    public class GetDeparmentByIdHandler : IRequestHandler<GetDepartmentByIdRequest , Department>
    {
        private readonly IDepartmentRepository _departmentRepository;
        public GetDeparmentByIdHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<Department> Handle(GetDepartmentByIdRequest request, CancellationToken cancellationToken)
        {
             var result = await  _departmentRepository.GetAllDepartmentById(request.Id);
             return result;
        }

        
    }
}
