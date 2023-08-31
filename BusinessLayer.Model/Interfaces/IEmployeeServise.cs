using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeInfo>> GetAllEmployees();
        Task<EmployeeInfo> GetEmployeeByCode(string companyCode);
        Task Post(EmployeeInfo employeeInfo);
        Task Put(EmployeeInfo employeeInfo);
        Task<bool> Delete(string companyCode);
    }
}
