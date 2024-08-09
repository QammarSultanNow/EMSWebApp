using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateEmployeeRequest request, CancellationToken cancellationToken)
        {

            var employee = _mapper.Map<EmployeeInformation>(request);

            var result = await _employeeRepository.UpdateEmplyeesRecord(employee, request.Id);
            return request.Id;
        }
    }
}
