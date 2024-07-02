using EMSWebApp.Models;

namespace EMSWebApp.Interface
{
    public interface IDepartmentRepository
    {
        Task<bool> AddDepartment(Department department);
        Task<IEnumerable<Department>> GetAllDepartment();
        Task<int> UpdateDepartment(Department department, int id);
        Task<Department> GetAllDepartmentById(int id);
        Task<int> DeleteDepartment(int id);
        Task<IEnumerable<DepartmentEmployeeCount>> EmployeeCount();
    }
}
