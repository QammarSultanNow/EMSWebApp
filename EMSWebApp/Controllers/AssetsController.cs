using ApplicationCore.Interfaces;
using ApplicationCore.AssetsModel;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;
using Microsoft.AspNetCore.Authorization;

namespace EMSWebApp.Controllers
{
    [Authorize]
    public class AssetsController : Controller
    {
        private readonly IAssetsRepository _assetsRepository;
        public AssetsController(IAssetsRepository assetsRepository)
        {
            _assetsRepository = assetsRepository;
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
        public async Task<IActionResult> AddAssets(Assets assets)
        {
            assets.CreatedAt = DateTime.Now;
            var result = await _assetsRepository.AddAssetsAsync(assets);
            if (result == 0)
            {
                throw new Exception("Record not added");
            }
            return RedirectToAction("GetAssetRecords");
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
        public async Task<IActionResult> UpdateAssetsRecords(Assets assets)
        {
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

            return View(result);
        }

        [HttpPost]
        [Route("Assets/AssignAssets")]
        public async Task<IActionResult> AssignAssets(EmployeeAssets employeeAssets)
        {
            var result = await _assetsRepository.AssignEmployeeAsset(employeeAssets);
            if (result > 0)
            {
                return RedirectToAction("ListAssestWithEmployee");
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
    }
}
