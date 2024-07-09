using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class DepartmentEmployeeCount
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public int EmployeeCount { get; set; }
        public int DepartmentCount { get; set; }
    }
    
}
