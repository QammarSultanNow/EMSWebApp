using ApplicationCore.AssetsModel;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Data
{
    public class ApplicationDbContext : IdentityDbContext , IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<EmployeeInformation> EmployeeInformationtbl { get; set; }
        public DbSet<Department> tblDepartment { get; set; }
        public DbSet<Assets> tblAssets { get; set; }
        public DbSet<EmployeeAssets> tblEmployeeAssets { get; set; }


        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

    }
   
}
