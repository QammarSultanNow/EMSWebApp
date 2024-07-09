using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<EmployeeInformation> EmployeeInformationtbl { get; set; }
        DbSet<Department> tblDepartment { get; set; }
        Task<int> SaveChangesAsync();


    }
    
}
