using EMSWebApp.IdentityModel;
using EMSWebApp.Interface;
using EMSWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EMSWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            
        }

        public DbSet<EmployeeInformation> EmployeeInformationtbl { get; set; }
        public DbSet<Department> tblDepartment { get; set; }


     public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

    }
}
