using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ApplicationCore.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<bool> AddEmplyee(EmployeeInformation employee);
        Task<IEnumerable<EmployeeViewModel>> GetAllEmployee(string userId, int id);
        Task<EmployeeInformation> GetAllEmployeeById(int id);
        Task<int> UpdateEmplyeesRecord(EmployeeInformation employee, int id);
        Task<int> DeleteEmployeesRecord(int id);
        Task<DepartmentEmployeeTotals> DepartmentEmployeeCounting();
    }
}
