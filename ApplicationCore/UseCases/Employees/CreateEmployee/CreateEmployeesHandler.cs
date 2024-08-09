using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Employees.CreateEmployee
{
    public class CreateEmployeesHandler : IRequestHandler<CreateEmployeesRequest, int>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public CreateEmployeesHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateEmployeesRequest request, CancellationToken cancellationToken)
        {
           var employee =  _mapper.Map<EmployeeInformation>(request);

           var result = await _employeeRepository.AddEmplyee(employee);
            return request.Id;
        }
    }
}
