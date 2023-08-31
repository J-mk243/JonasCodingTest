using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System.Globalization;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CompanyInfo>> GetAllCompanies()
        {
            var res = await _companyRepository.GetAll();
            return _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }

        public async Task<CompanyInfo> GetCompanyByCode(string companyCode)
        {
            var result = await _companyRepository.GetByCode(companyCode);
            return _mapper.Map<CompanyInfo>(result);
        }

        public async Task Post(CompanyInfo companyInfo)
        {
            var _newCompany = _mapper.Map<Company>(companyInfo);
            await _companyRepository.SaveCompany(_newCompany);
        }

        public async Task Put(CompanyInfo companyInfo)
        {
            var _companyToUpdate = _mapper.Map<Company>(companyInfo);
            await _companyRepository.SaveCompany(_companyToUpdate);
        }
        public async Task<bool> Delete(string companyCode)
        {
            return await _companyRepository.Delete(companyCode);
        }
    }
}
