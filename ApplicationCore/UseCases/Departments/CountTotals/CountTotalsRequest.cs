using ApplicationCore.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Departments.CountTotals
{
    public class CountTotalsRequest : IRequest<IEnumerable<DepartmentEmployeeCount>>
    {
    }
}
