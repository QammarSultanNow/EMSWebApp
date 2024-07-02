using EMSWebApp.Data;
using EMSWebApp.Interface;
using EMSWebApp.Models;
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
            var result = await _context.tblDepartment.ToListAsync();
            return result;

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
    }
}
