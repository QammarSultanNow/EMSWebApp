using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Employees.EmployeesExportExcel
{
    public class EmployeesExportExcelRequest : IRequest<byte[]>
    {
        public string UserId { get; set; }
        public int  Id { get; set; }

        public EmployeesExportExcelRequest(string userId, int id)
        {
            this.UserId = userId;
            this.Id = id;
        }
    }
}
