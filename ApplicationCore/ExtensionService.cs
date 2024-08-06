using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore
{
    public static class ExtensionService
    {
        public static void RegisterApplicationCoreServices(this IServiceCollection services)
        {

            // Add Mediator
            services.AddMediatR(conf => conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
           



        }
    }
}
