using ApplicationCore.AssetsModel;
using ApplicationCore.Models;
using ApplicationCore.UseCases.Assets.AddAssignedAsset;
using ApplicationCore.UseCases.Assets.CreateAssets;
using ApplicationCore.UseCases.Assets.UpdateAssets;
using ApplicationCore.UseCases.Departments.CreateDepartment;
using ApplicationCore.UseCases.Departments.UpdateDepartment;
using ApplicationCore.UseCases.Employees.CreateEmployee;
using ApplicationCore.UseCases.Employees.UpdateEmployees;
using ApplicationCore.UseCases.Users.UpdateUser;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateEmployeesRequest, EmployeeInformation>();
            CreateMap<UpdateEmployeeRequest, EmployeeInformation>();
            CreateMap<CreateDepartmentRequest, Department>();
            CreateMap<UpdateDepartmentRequest, Department>();
            CreateMap<AddAssignedAssetRequest, EmployeeAssets>();
            CreateMap<CreateAssetsRequst, Assets>();
            CreateMap<UpdateAssetsRequest , Assets>();
            CreateMap<UpdateUserRequest, IdentityUser>();

        }
    }
}
