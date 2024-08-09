using ApplicationCore.Models;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public UpdateDepartmentHandler(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateDepartmentRequest request, CancellationToken cancellationToken)
        {
          var department =   _mapper.Map<Department>(request);

           var result = await _departmentRepository.UpdateDepartment(department, request.Id);
            return result;
        }
    }
}
