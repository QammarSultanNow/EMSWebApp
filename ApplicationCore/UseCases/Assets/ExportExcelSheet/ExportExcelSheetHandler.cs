using ApplicationCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Assets.ExportExcelSheet
{
    public class ExportExcelSheetHandler : IRequestHandler<ExportExcelSheetRequest, byte[]>
    {
        private readonly IExportEmployeeExcelSheet _exportAseetExcelSheet;

        public ExportExcelSheetHandler(IExportEmployeeExcelSheet exportAseetExcelSheet)
        {
            _exportAseetExcelSheet = exportAseetExcelSheet;
        }
        public async Task<byte[]> Handle(ExportExcelSheetRequest request, CancellationToken cancellationToken)
        {
            byte[] result = await _exportAseetExcelSheet.DownloadDAssetExcelSheet();
            return result;
        }
    }
}
