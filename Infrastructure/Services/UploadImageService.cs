using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class UploadImageService : IUploadImageService
    {
        public async Task UploadImageByUser(EmployeeInformation employee, [FromForm] IFormFile image)
        {
            if (image == null)
            {
                employee.ImagePath = null;
            }
            else
            {
                string ImageName = System.IO.Path.GetFileName(image.FileName);
                string Path = "wwwroot\\" + ImageName;
                using (var stream = new FileStream(Path, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                employee.ImagePath = ImageName;
            }
        }


    }
    
}
