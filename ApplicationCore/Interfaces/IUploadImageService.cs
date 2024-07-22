using ApplicationCore.AssetsModel;
using ApplicationCore.Models;
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
        Task UploadImageByUser(EmployeeInformation employee, [FromForm] IFormFile image);
        Task UploadAssetImage(Assets asset, [FromForm] IFormFile image);
    }
    
}
