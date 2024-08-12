using ApplicationCore.AssetsModel;
using ApplicationCore.Models;
using ApplicationCore.UseCases.Assets.CreateAssets;
using ApplicationCore.UseCases.Assets.UpdateAssets;
using ApplicationCore.UseCases.Employees.CreateEmployee;
using ApplicationCore.UseCases.Employees.UpdateEmployees;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUploadImageService
    {
        Task UploadImageByUser(CreateEmployeesRequest request);
        Task UpdateImageByUser(UpdateEmployeeRequest request);
        Task UploadAssetImage(CreateAssetsRequst requst);
        Task UpdateAssetImage(UpdateAssetsRequest request);
    }
    
}
