using ApplicationCore.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Departments.GetDepartmentById
{
    public class GetDepartmentByIdRequest : IRequest<Department>
    {
        public int Id { get; set; }
    }
}
