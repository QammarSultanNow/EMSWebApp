using ApplicationCore.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Departments.GetDepartment
{
    public class GetDepartmentRequest :IRequest<IEnumerable<DepartmentEmployeeCount>>
    {
    }
}
