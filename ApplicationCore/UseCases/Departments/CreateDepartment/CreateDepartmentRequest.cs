using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Departments.CreateDepartment
{
    public class CreateDepartmentRequest : IRequest<int>
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
    }
}
