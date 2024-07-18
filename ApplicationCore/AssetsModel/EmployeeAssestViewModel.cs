using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.AssetsModel
{
    
    public class EmployeeAssestViewModel
    {
        public List<Assets> Assets { get; set; }
        public List<EmployeeInformation> Employees { get; set; }
    }
}
