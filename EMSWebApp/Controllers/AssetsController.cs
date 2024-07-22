using ApplicationCore.Interfaces;
using ApplicationCore.AssetsModel;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Enum;
using Infrastructure.Services;
using EMSWebApp.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EMSWebApp.Controllers
{
    [Authorize]
    public class AssetsController : Controller
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IExportEmployeeExcelSheet _exportAseetExcelSheet;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUploadImageService _uploadImageService;
        private readonly IDepartmentRepository _departmentRepository;

        public AssetsController(IAssetsRepository assetsRepository, UserManager<IdentityUser> userManager, IExportEmployeeExcelSheet exportAseetExcelSheet, IUploadImageService uploadImageService, IDepartmentRepository departmentRepository)
        {
            _assetsRepository = assetsRepository;
            _userManager = userManager;
            _exportAseetExcelSheet = exportAseetExcelSheet;
            _uploadImageService = uploadImageService;
            _departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AddAssets()
        {
            return View();
        }

        [HttpPost]
        [Route("Assets/AddAssets")]
        public async Task<IActionResult> AddAssets(Assets assets , [FromForm] IFormFile image)
        {
            await _uploadImageService.UploadAssetImage(assets , image);

            assets.CreatedAt = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            assets.CreatedBy = _userManager.GetUserName(User);
            assets.Status = EssetEnum.Available.ToString();

            var res =  await _assetsRepository.AddAssetsAsync(assets);
            if(res > 0)
            {
               return RedirectToAction("GetAssetRecords");
            }

            throw new Exception("Record not added");
        }

        public async Task<IActionResult> GetAssetRecords()
        {
            try
            {
                var result = await _assetsRepository.GetAssetRecords();
                if (result == null)
                {
                    throw new Exception();
                }
               
                return View(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public async Task<IActionResult> GetAssetsRecordById(int id)
        {
            var result = await _assetsRepository.GetAssetsRecordById(id);
            return View(result);
        }

        [HttpPost]
        [Route("Assets/UpdateAssetsRecords")]
        public async Task<IActionResult> UpdateAssetsRecords(Assets assets , [FromForm] IFormFile image)
        {
            await _uploadImageService.UploadAssetImage(assets, image);


            var result = await _assetsRepository.UpdateAssetsRecords(assets);
            return RedirectToAction("GetAssetRecords");
        }


        [Route("Assets/DeleteAssetsRecords/{id}")]
        public async Task<IActionResult> DeleteAssetsRecords(int id)
        {
            var result = await _assetsRepository.DeleteAssetsRecords(id);
            if (result > 0)
            {
                return RedirectToAction("GetAssetRecords");
            }
            return View("AddAssets");
        }


        public async Task<IActionResult> AssignAssets(int id)
        {
            var result = await _assetsRepository.EmployeeAssetList(id);
            var dptData =  await _departmentRepository.GetAllDepartment();
            ViewBag.Department = dptData;

            return View(result);
        }

        [HttpPost]
        [Route("Assets/AssignAssets")]
        public async Task<IActionResult> AssignAssets(EmployeeAssets employeeAssets)
        {
            var result = await _assetsRepository.AssignEmployeeAsset(employeeAssets);
            if (result > 0)
            {
                return RedirectToAction("GetAssetRecords");
            }
            return View();
        }

        public async Task<IActionResult> ListAssestWithEmployee(int id)
        {

            var result = await _assetsRepository.ListAssetandEmployee(id);
            return View(result);
        }

        public async Task<IActionResult> UnassignAsset(int id)
        {

            var result = await _assetsRepository.UnassignedAssetRepo(id);
            return RedirectToAction("GetAssetRecords");
        }


        [Route("Assets/AssetExcelSheet")]
        public async Task<IActionResult> AssetExcelSheet()
        {
            byte[] excelSheet = await _exportAseetExcelSheet.DownloadDAssetExcelSheet();
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "Employees.xlsx";

            return File(excelSheet, contentType, fileName);
        }



        [HttpGet]
        [Route("Assets/GetEmployeeListOnDepartmentId")]
        public async Task<IActionResult> GetEmployeeListOnDepartmentId(int departmentId)
        {
          var result =  await _assetsRepository.GetEmployeeListOnDepartmentIdRepo(departmentId);
          return View(result);
        }
    }
}
