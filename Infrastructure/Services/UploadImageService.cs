using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ApplicationCore.AssetsModel;
using ApplicationCore.UseCases.Assets.CreateAssets;
using ApplicationCore.UseCases.Assets.UpdateAssets;
using ApplicationCore.UseCases.Employees.CreateEmployee;
using ApplicationCore.UseCases.Employees.UpdateEmployees;

namespace Infrastructure.Services
{
    public class UploadImageService : IUploadImageService
    {
        public async Task UploadImageByUser(CreateEmployeesRequest request)
        {
            if (request.Image == null)
            {
                request.ImagePath = null;
            }
            else
            {
                string ImageName = System.IO.Path.GetFileName(request.Image.FileName);
                string Path = "wwwroot\\" + ImageName;
                using (var stream = new FileStream(Path, FileMode.Create))
                {
                    request.Image.CopyTo(stream);
                }
                request.ImagePath = ImageName;
            }
        }

        public async Task UpdateImageByUser(UpdateEmployeeRequest request)
        {
            if (request.Image == null)
            {
                request.ImagePath = null;
            }
            else
            {
                string ImageName = System.IO.Path.GetFileName(request.Image.FileName);
                string Path = "wwwroot\\" + ImageName;
                using (var stream = new FileStream(Path, FileMode.Create))
                {
                    request.Image.CopyTo(stream);
                }
                request.ImagePath = ImageName;
            }
        }


        public async Task UploadAssetImage(CreateAssetsRequst request)
        {
            if (request.Image == null)
            {
                request.ImagePath = null;
            }
            else
            {
                string ImageName = System.IO.Path.GetFileName(request.Image.FileName);
                string Path = "wwwroot\\" + ImageName;
                using (var stream = new FileStream(Path, FileMode.Create))
                {
                    request.Image.CopyTo(stream);
                }
                request.ImagePath = ImageName;
            }
        }

        public async Task UpdateAssetImage(UpdateAssetsRequest request)
        {
            if (request.Image == null)
            {
                request.ImagePath = null;
            }
            else
            {
                string ImageName = System.IO.Path.GetFileName(request.Image.FileName);
                string Path = "wwwroot\\" + ImageName;
                using (var stream = new FileStream(Path, FileMode.Create))
                {
                    request.Image.CopyTo(stream);
                }
                request.ImagePath = ImageName;
            }
        }

    }
    
}
