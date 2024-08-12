using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Assets.ExportExcelSheet
{
    public class ExportExcelSheetRequest :IRequest<byte[]>
    {
    }
}
