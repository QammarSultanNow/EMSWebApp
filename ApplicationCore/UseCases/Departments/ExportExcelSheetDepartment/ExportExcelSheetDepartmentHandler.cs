using ApplicationCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Departments.ExportExcelSheetDepartment
{
    public class ExportExcelSheetDepartmentHandler : IRequestHandler<ExportExcelSheetDepartmentRequest, byte[]>
    {
        private readonly IExportEmployeeExcelSheet _exportAseetExcelSheet;
        public ExportExcelSheetDepartmentHandler(IExportEmployeeExcelSheet exportAseetExcelSheet)
        {
            _exportAseetExcelSheet = exportAseetExcelSheet;
        }
        public async Task<byte[]> Handle(ExportExcelSheetDepartmentRequest request, CancellationToken cancellationToken)
        {
          byte[] result = await  _exportAseetExcelSheet.DownloadDepartmentExcelSheet();
          return result;   
        }
    }
}
