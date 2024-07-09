using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Emp_Email { get; set; }
        public string Adress { get; set; }
        public string Designation { get; set; }
        public string ContactNo { get; set; }
        public string ImagePath { get; set; }
        public Department Department { get; set; }
        public string CreatedOn { get; set; }
        public string UserName { get; set; }
    }
   
}
