using ApplicationCore.AssetsModel;
using ApplicationCore.Data;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Infrastructure.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AssetsRepository : IAssetsRepository
    {
        private readonly ApplicationDbContext _context;
        public AssetsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAssetsAsync(Assets assets)
        {
            await _context.tblAssets.AddAsync(assets);
            await _context.SaveChangesAsync();
            return assets.Id;
        }

        //public async Task<IEnumerable<Assets>> GetAllAssetData()
        //{
        //    var assestList = await _context.tblAssets.ToListAsync();
        //    return assestList;
        //}

        public async Task<IEnumerable<ListEmployeeAssetViewModel>> GetAssetRecords()
        {

            var res = from a in _context.tblAssets
                      join e in _context.tblEmployeeAssets on a.Id equals e.AssetId into empAssetJoin
                      from e in empAssetJoin.DefaultIfEmpty()


                      join emp in _context.EmployeeInformationtbl
                      on e.EmployeeId equals emp.Id into empJoin
                      from emp in empJoin.DefaultIfEmpty()
                      select new ListEmployeeAssetViewModel

                      {
                          Id = a.Id,
                          Asset_Name = a.Name,
                          Description = a.Description,
                          PurchasingPrice = a.PurchasingPrice,
                          Status = a.Status,
                          ImagePath = a.ImagePath,  
                          Emp_Id = emp.Id,
                          Emp_Name = emp.Name,
                          CreatedAt =DateTime.Parse(a.CreatedAt),
                          CreatedBy = a.CreatedBy
                      };

            return res;
           
            
        }

        public async Task<Assets> GetAssetsRecordById(int id)
        {
            var assestList = await _context.tblAssets.Where(x => x.Id == id).FirstOrDefaultAsync();
            return assestList;
        }

        public async Task<int> UpdateAssetsRecords(Assets assets)
        {
            var result = await _context.tblAssets.Where(x => x.Id == assets.Id).FirstOrDefaultAsync();

            result.Name = assets.Name;
            result.Description = assets.Description;
            result.PurchasingPrice = assets.PurchasingPrice;
            //result.Status = assets.Status;
            if (assets.ImagePath != null)
            {
                result.ImagePath = assets.ImagePath;
            }
            _context.tblAssets.Update(result);
            await _context.SaveChangesAsync();
            return result.Id;
        }

        public async Task<int> DeleteAssetsRecords(int id)
        {
            var result = await _context.tblAssets.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result != null)
            {
               if (result.Status == "Assigned")
               {
                   var res =   await _context.tblEmployeeAssets.Where(x => x.AssetId == id).FirstOrDefaultAsync();
                    _context.tblEmployeeAssets.Remove(res);
                    
                }

                _context.tblAssets.Remove(result);
                await _context.SaveChangesAsync();
                return result.Id;
            }
              return 0;
        }

        public async Task<EmployeeAssestViewModel> EmployeeAssetList(int? id)
        {
            IEnumerable<Assets> asset;

            if (id > 0)
            {
                 asset = await _context.tblAssets.Where(x => x.Id == id).ToListAsync();
            }
            else
            {
                asset = await _context.tblAssets.ToListAsync();
            }
            var empolyee = await _context.EmployeeInformationtbl.ToListAsync();
            


            return new EmployeeAssestViewModel
            {
                Assets = asset.ToList(),
                Employees = empolyee,
            };
        }

        public async Task<int> AssignEmployeeAsset(EmployeeAssets assignEmployeeAssets)
        {
            var asset = await _context.tblAssets.Where(x => x.Id == assignEmployeeAssets.AssetId).FirstOrDefaultAsync();
            asset.Status = EssetEnum.Assigned.ToString();

            _context.tblAssets.Update(asset);


            await _context.tblEmployeeAssets.AddAsync(assignEmployeeAssets);
            await _context.SaveChangesAsync();
            return assignEmployeeAssets.Id;
        }

        public async Task<IEnumerable<ListEmployeeAssetViewModel>> ListAssetandEmployee(int? id)
        {
            IEnumerable<EmployeeAssets> list;
            if (id > 0)
            {
                list = await _context.tblEmployeeAssets.Where(x => x.EmployeeId == id).ToListAsync();
            }
            else
            {
                list = await _context.tblEmployeeAssets.ToListAsync();
            }

            var res = from e in list
                      join asset in _context.tblAssets
                      on e.AssetId equals asset.Id
                      join emp in _context.EmployeeInformationtbl
                      on e.EmployeeId equals emp.Id
                      select new ListEmployeeAssetViewModel
                      {
                          Id = e.AssetId,
                          Asset_Name = asset.Name,
                          Description = asset.Description,
                          PurchasingPrice = asset.PurchasingPrice,
                          Status = asset.Status,
                          Emp_Name = emp.Name,
                          CreatedAt = DateTime.Parse(asset.CreatedAt),
                          CreatedBy = asset.CreatedBy,
                      };
            return res;
        }

        public async Task<int> UnassignedAssetRepo(int id)
        {
         var result = await _context.tblAssets.Where(x => x.Id == id).FirstOrDefaultAsync();

            result.Status = EssetEnum.Available.ToString();
            _context.tblAssets.Update(result);
            


            var removeResult = await _context.tblEmployeeAssets.Where(x => x.AssetId == id).FirstOrDefaultAsync();
            _context.tblEmployeeAssets.Remove(removeResult);
            await _context.SaveChangesAsync();

            return result.Id;

        }

       public async Task<IEnumerable<EmployeeInformation>> GetEmployeeListOnDepartmentIdRepo(int id)
        {
            
              var result = await _context.EmployeeInformationtbl.Where(x => x.DepartmentId == id).ToListAsync();
              return result;
        }

    }
    
}
