using EMSWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EMSWebApp.Interface
{
    public interface IApplicationDbContext
    {
        DbSet<EmployeeInformation> EmployeeInformationtbl { get; set; }
        DbSet<Department> tblDepartment { get; set; }
        Task<int> SaveChangesAsync();

        IDbConnection CreateConnection();
    }
}
