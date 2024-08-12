using ApplicationCore.Interfaces;
using Azure.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Employees.EmployeesExportExcel
{
    public class EmployeesExportExcelHandler : IRequestHandler<EmployeesExportExcelRequest, byte[]>
    {
        private readonly IExportEmployeeExcelSheet _exportEmployeeExcel;
        public EmployeesExportExcelHandler(IExportEmployeeExcelSheet exportEmployeeExcel)
        {
            _exportEmployeeExcel = exportEmployeeExcel;
        }
        public async Task<byte[]> Handle(EmployeesExportExcelRequest request, CancellationToken cancellationToken)
        {
            byte[] excelSheet = await _exportEmployeeExcel.DownloadEmployeeExcelSheet(request.UserId, request.Id);
            return excelSheet;  
        }
    }
}
