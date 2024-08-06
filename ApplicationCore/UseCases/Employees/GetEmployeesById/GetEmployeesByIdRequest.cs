using ApplicationCore.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Employees.GetEmployeesById
{
    public class GetEmployeesByIdRequest :IRequest<EmployeeInformation>
    {
        public int Id { get; set; }
    }
}
