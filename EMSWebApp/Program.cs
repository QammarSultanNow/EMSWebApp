using ApplicationCore;
using Infrastructure;
using ApplicationCore.Data;
using ApplicationCore.Interfaces;
using EMSWebApp.Interface;
using EMSWebApp.Repository;
using EMSWebApp.Roles;
using EMSWebApp.Seed;
using EMSWebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Infrastructure.Repositories;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.AspNetCore.Builder;
using static System.Net.Mime.MediaTypeNames;


namespace EMSWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);
            //Add Extension Method
            builder.Services.RegisterApplicationCoreServices();
            builder.Services.RegisterInfrastructureServices();
            builder.Services.AddScoped<IEmailSender, EmailVerificationService>();


            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();



            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .Enrich.WithThreadName()
                .Enrich.WithThreadId()
                .Enrich.WithEnvironmentUserName()
                .WriteTo.Console(new CompactJsonFormatter())
                .WriteTo.File(new CompactJsonFormatter(), "Log/log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.MSSqlServer(
                  connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
                  sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true },
                   columnOptions: new ColumnOptions
                   {
                       AdditionalColumns = new Collection<SqlColumn>
                        {
                           new SqlColumn{ColumnName = "MachineName", DataType = System.Data.SqlDbType.NVarChar, DataLength = 128},
                           new SqlColumn{ColumnName = "EnvironmentName" , DataType = System.Data.SqlDbType.NVarChar, DataLength = 128}
                        }
                   },
                  restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information

                  ).CreateLogger();

            builder.Logging.AddSerilog(Log.Logger);
            Log.Information("SERILOG IS WORKING FINE");






            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;

            })
          .AddRoles<IdentityRole>()
          .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();

            SeedRoles.DefaultRoles(builder.Services.BuildServiceProvider());
            SeedUsers.DefaultUser(builder.Services.BuildServiceProvider());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.UseExceptionHandler("Error/error");
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/Error/NotFound");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
