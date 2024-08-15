using ApplicationCore.Models;
using ApplicationCore.UseCases.Employees.CreateEmployee;
using ApplicationCore.ValidationBehavior;
using ApplicationCore.Validations;
using Azure.Core;
using FluentValidation;
using MediatR;
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
            //Add AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //Add Fluent Validations
            services.AddValidatorsFromAssemblyContaining<CreateEmployeesRequestValidators>()                                                                ;

            // Add Mediator
            services.AddMediatR(conf => conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


        }
    }
}
