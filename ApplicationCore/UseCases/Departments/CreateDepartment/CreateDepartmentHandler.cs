using ApplicationCore.Models;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public CreateDepartmentHandler(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateDepartmentRequest request, CancellationToken cancellationToken)
        {
            var department = _mapper.Map<Department>(request);

            var result = await _departmentRepository.AddDepartment(department);
            return request.Id;
        }
    }
}
