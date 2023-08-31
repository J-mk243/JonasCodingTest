using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyInfo>> GetAllCompanies();
        Task<CompanyInfo> GetCompanyByCode(string companyCode);
        Task Post(CompanyInfo companyInfo);
        Task Put(CompanyInfo companyInfo);
        Task<bool> Delete(string companyCode);
    }
}
