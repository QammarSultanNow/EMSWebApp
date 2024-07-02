using EMSWebApp.Interface;
using EMSWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EMSWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext , IApplicationDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(connectionString);

        public DbSet<EmployeeInformation> EmployeeInformationtbl { get; set; }
        public DbSet<Department> tblDepartment { get; set; }


     public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

    }
}
