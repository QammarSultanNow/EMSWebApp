using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Departments.DeleteDepartment
{
    public class DeleteDepartmentRequest : IRequest<int>
    {
        public int Id { get; set; }
    }
}
