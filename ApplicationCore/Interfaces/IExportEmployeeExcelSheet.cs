using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IExportEmployeeExcelSheet
    {
        Task<byte[]> DownloadEmployeeExcelSheet(string userId, int id);
        Task<byte[]> DownloadDepartmentExcelSheet();
        Task<byte[]> DownloadDAssetExcelSheet();
    }
}
