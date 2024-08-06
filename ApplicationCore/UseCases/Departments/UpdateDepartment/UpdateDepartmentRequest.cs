using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Departments.UpdateDepartment
{
    public class UpdateDepartmentRequest : IRequest<int>
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
    }
}
