using EMSWebApp.Models;

namespace EMSWebApp.Interface
{
    public interface IEmployeeRepository
    {
        Task<bool> AddEmplyee(EmployeeInformation employee);
        Task<IEnumerable<EmployeeViewModel>> GetAllEmployee(int id);
        Task<EmployeeInformation> GetAllEmployeeById(int id);
        Task<int> UpdateEmplyeesRecord(EmployeeInformation employee, int id);
        Task<int> DeleteEmployeesRecord(int id);
        Task<DepartmentEmployeeTotals> DepartmentEmployeeCounting();

    }
}
