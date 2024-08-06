using ApplicationCore.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Employees.GetEmployees
{
    public class GetEmployeeRequest :IRequest<IEnumerable<EmployeeViewModel>>
    {
        public string UserID { get; set; }
        public int Id { get; set; }

        public GetEmployeeRequest(string userId,int id)
        {
            this.UserID = userId;
            this.Id = id;
        }
    }
}
