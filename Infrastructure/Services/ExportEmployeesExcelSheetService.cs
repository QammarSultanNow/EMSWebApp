using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using EMSWebApp.Interface;
using Infrastructure.Repositories;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public  class ExportEmployeesExcelSheetService : IExportEmployeeExcelSheet
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _dptRrepository;
        private readonly IAssetsRepository _assetsRepository;
        public ExportEmployeesExcelSheetService(IEmployeeRepository employeeRepository, IDepartmentRepository dptRrepository, IAssetsRepository assetsRepository)
        {
            _employeeRepository = employeeRepository;
            _dptRrepository = dptRrepository;
            _assetsRepository = assetsRepository;
        }

        public async Task<byte[]> DownloadDepartmentExcelSheet()
        {
           var department = await _dptRrepository.GetAllDepartment();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();
            using (var package =new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Department");

                worksheet.Cells[1, 1].Value = "Department Name";
                int row = 2;
                foreach(var dpt in department)
                {
                  worksheet.Cells[row, 1].Value = dpt.DepartmentName;
                  row++;
                }

                return package.GetAsByteArray();
            }
        }

        public async  Task<byte[]> DownloadEmployeeExcelSheet(string userId , int id)
        {
            var employees = await _employeeRepository.GetAllEmployee(userId,  id);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("EmployeeViewModel");

                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "Email";
                worksheet.Cells[1, 3].Value = "Adress";
                worksheet.Cells[1, 4].Value = "Designation";
                worksheet.Cells[1, 5].Value = "ContactNo";
                worksheet.Cells[1, 6].Value = "DepartmentName";
                worksheet.Cells[1, 7].Value = "CreatedBy";
                worksheet.Cells[1, 8].Value = "CreatedOn";

                int row = 2;
                foreach (var emp in employees)
                {
                    worksheet.Cells[row, 1].Value = emp.Name;
                    worksheet.Cells[row, 2].Value = emp.Emp_Email;
                    worksheet.Cells[row, 3].Value = emp.Adress; // Corrected "Adress" to "Address"
                    worksheet.Cells[row, 4].Value = emp.Designation;
                    worksheet.Cells[row, 5].Value = emp.ContactNo;
                    worksheet.Cells[row, 6].Value = emp.Department.DepartmentName;
                    worksheet.Cells[row, 7].Value = emp.UserName;
                    worksheet.Cells[row, 8].Value = emp.CreatedOn.ToString("dddd, MMMM d, yyyy h:mm tt");
                    row++;

                }
                
             return package.GetAsByteArray();
            }
        }


        public async Task<byte[]> DownloadDAssetExcelSheet()
        {
            var asset = await _assetsRepository.GetAssetRecords();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("ListEmployeeAssetViewModel");

                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "Description";
                worksheet.Cells[1, 3].Value = "Purchasing Price";
                worksheet.Cells[1, 3].Value = "Assign To";
                worksheet.Cells[1, 4].Value = "Status";
                worksheet.Cells[1, 5].Value = "Created At";
                worksheet.Cells[1, 6].Value = "Created By";
                int row = 2;
                foreach (var ast in asset)
                {
                    worksheet.Cells[row, 1].Value = ast.Asset_Name;
                    worksheet.Cells[row, 2].Value = ast.Description;
                    worksheet.Cells[row, 3].Value = ast.PurchasingPrice;
                    worksheet.Cells[row, 3].Value = string.IsNullOrEmpty(ast.Emp_Name) ? "Unassigned" : ast.Emp_Name;
                    worksheet.Cells[row, 4].Value = ast.Status;
                    worksheet.Cells[row, 5].Value = ast.CreatedAt.ToString("dddd, MMMM d, yyyy h:mm tt");
                    worksheet.Cells[row, 6].Value = ast.CreatedBy;

                    row++;
                }

                return package.GetAsByteArray();
            }
        }

    }
}
