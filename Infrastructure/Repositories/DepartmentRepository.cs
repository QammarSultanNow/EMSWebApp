using ApplicationCore.Data;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using EMSWebApp.Interface;
using Microsoft.EntityFrameworkCore;

namespace EMSWebApp.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    { 
        private readonly IApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddDepartment(Department department)
        {
          var result =  await _context.tblDepartment.AddAsync(department);
          await _context.SaveChangesAsync();
            if (result != null)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Department>> GetAllDepartment()
        {
            try
            {
                var result = await _context.tblDepartment.ToListAsync();
                return result;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<Department> GetAllDepartmentById(int id)
        {
            var result = await _context.tblDepartment.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<int> UpdateDepartment(Department department, int id)
        {
            var result = await _context.tblDepartment.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result == null)
            {
                throw new Exception("");
            }
            result.DepartmentName = department.DepartmentName;
            await _context.SaveChangesAsync();
            return result.Id;
        }

        public async Task<int> DeleteDepartment(int id)
        {
            var result = await _context.tblDepartment.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result == null)
            {
                throw new Exception("");
            }
            _context.tblDepartment.Remove(result);
            await _context.SaveChangesAsync();
            return result.Id;
        }

        public async Task<IEnumerable<DepartmentEmployeeCount>> EmployeeCount()
        {
            try
            {
                var result = await _context.tblDepartment.ToListAsync();
                var res = (from dpt in result
                           join emp in _context.EmployeeInformationtbl
                           on dpt.Id equals emp.DepartmentId into deptGroup
                           select new DepartmentEmployeeCount
                           {
                               Id = dpt.Id,
                               DepartmentName = dpt.DepartmentName,
                               EmployeeCount = deptGroup.Count()
                           }).ToList();

                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
