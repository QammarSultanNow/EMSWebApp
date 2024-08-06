using Azure.Core;
using EMSWebApp.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Departments.DeleteDepartment
{
    public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentRequest, int>
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DeleteDepartmentHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<int> Handle(DeleteDepartmentRequest request, CancellationToken cancellationToken)
        {
          var result =  await _departmentRepository.DeleteDepartment(request.Id);
          return result;
        }
    }
}
