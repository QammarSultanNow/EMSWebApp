using EMSWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EMSWebApp.Interface
{
    public interface IUploadImageService
    {
        Task UploadImageByUser(EmployeeInformation employee, [FromForm] IFormFile image);
    }
}
