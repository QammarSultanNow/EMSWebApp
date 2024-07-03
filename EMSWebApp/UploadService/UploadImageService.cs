using EMSWebApp.Interface;
using EMSWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EMSWebApp.UploadService
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
