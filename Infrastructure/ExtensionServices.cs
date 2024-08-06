using ApplicationCore.Interfaces;
using EMSWebApp.Interface;
using EMSWebApp.Repository;
using EMSWebApp.Services;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ExtensionServices
    {
        public static void RegisterInfrastructureServices(this IServiceCollection services)
        {
            
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUploadImageService, UploadImageService>();
            services.AddScoped<IExportEmployeeExcelSheet, ExportEmployeesExcelSheetService>();
            services.AddScoped<IAssetsRepository, AssetsRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IEmailService, EmailService>();
           
        }
    }
}
