using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Data;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddEmplyee(EmployeeInformation employee)
        {

            var results = await _context.EmployeeInformationtbl.AddAsync(employee);
            await _context.SaveChangesAsync();
            if (results != null)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetAllEmployee(string userId, int id)
        {
            IQueryable<EmployeeInformation> employee;
            if (userId != null)
            {
                employee = _context.EmployeeInformationtbl.Include("Department").Where(x => x.CreatedBy == userId);
            }

            else
            {
                if (id > 0)
                {
                    employee = _context.EmployeeInformationtbl.Include("Department").Where(x => x.DepartmentId == id);
                }
                else
                {
                    employee = _context.EmployeeInformationtbl.Include("Department");
                }
            }

            var res = (from emp in employee
                       join usr in _context.Users
                       on emp.CreatedBy equals usr.Id
                       select new EmployeeViewModel
                       {
                           Id = emp.Id,
                           Name = emp.Name,
                           Emp_Email = emp.Email,
                           Adress = emp.Adress,
                           Designation = emp.Designation,
                           ContactNo = emp.ContactNo,
                           ImagePath = emp.ImagePath,
                           Department = emp.Department,
                           CreatedOn = DateTime.Parse(emp.CreatedOn),
                           UserName = usr.UserName,
                       }).ToList();

            return (IEnumerable<EmployeeViewModel>)res;
        }


        public async Task<EmployeeInformation> GetAllEmployeeById(int id)
        {
            var result = await _context.EmployeeInformationtbl.Include("Department").FirstOrDefaultAsync(e => e.Id == id);
            if (result == null)
            {
                return null;
            }
            return result;
        }


        public async Task<int> UpdateEmplyeesRecord(EmployeeInformation employee, int id)
        {
            var result = await _context.EmployeeInformationtbl.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result == null)
            {
                throw new Exception("");
            }
              
                result.Name = employee.Name;
                result.Email = employee.Email;
                result.Adress = employee.Adress;
                result.Designation = employee.Designation;
                result.ModifiedOn = employee.ModifiedOn;
                result.ModifiedBy = employee.ModifiedBy;
                result.ContactNo = employee.ContactNo;
                result.DepartmentId = employee.DepartmentId;
            
             if (employee.ImagePath != null)
                {
                    result.ImagePath = employee.ImagePath;
                }


            await _context.SaveChangesAsync();
            return result.Id;
        }

        public async Task<int> DeleteEmployeesRecord(int id)
        {
            var result = await _context.EmployeeInformationtbl.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result == null)
            {
                throw new Exception("");
            }
            _context.EmployeeInformationtbl.Remove(result);
            await _context.SaveChangesAsync();
            return result.Id;

        }

        public async Task<DepartmentEmployeeTotals> DepartmentEmployeeCounting()
        {
            var dptCount = await _context.tblDepartment.CountAsync();
            var empCount = await _context.EmployeeInformationtbl.CountAsync();
            var assetCount = await _context.tblAssets.CountAsync();
            var assignedAssetCount = await _context.tblEmployeeAssets.CountAsync();

            var result = new DepartmentEmployeeTotals
            {
                DepartmentCount = dptCount,
                EmployeeCount = empCount,
                AssetCount = assetCount,
                AssignedAssetCount = assignedAssetCount
            };
            return result;
        }

        public async Task<IEnumerable<EmployeeInformation>> GetEmployeeByDepartmentId(int id)
        {
           var result = await _context.EmployeeInformationtbl.Where(x => x.DepartmentId == id).ToListAsync();
            return result;
        }
    }
}


