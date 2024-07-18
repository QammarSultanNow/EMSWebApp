using ApplicationCore.AssetsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAssetsRepository
    {
        Task<int> AddAssetsAsync(Assets assets);
        Task<IEnumerable<Assets>> GetAssetRecords(int? id);
        Task<Assets> GetAssetsRecordById(int id);
        Task<int> UpdateAssetsRecords(Assets assets);
        Task<int> DeleteAssetsRecords(int id);
        Task<EmployeeAssestViewModel> EmployeeAssetList(int? id);
        Task<int> AssignEmployeeAsset(EmployeeAssets assignEmployeeAssets);
        Task<IEnumerable<ListEmployeeAssetViewModel>> ListAssetandEmployee(int id);
    }
    
}
