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
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployees()
        {
            var res = await _employeeRepository.GetAll();
            return _mapper.Map<IEnumerable<EmployeeInfo>>(res);
        }
        public async Task<EmployeeInfo> GetEmployeeByCode(string companyCode)
        {
            var result = await _employeeRepository.GetByCode(companyCode);
            return _mapper.Map<EmployeeInfo>(result);
        }

        public async Task Post(EmployeeInfo employeeInfo)
        {
            var _newCompany = _mapper.Map<Employee>(employeeInfo);
            await _employeeRepository.SaveEmployee(_newCompany);
        }

        public async Task Put(EmployeeInfo employeeInfo)
        {
            var _companyToUpdate = _mapper.Map<Employee>(employeeInfo);
            await _employeeRepository.SaveEmployee(_companyToUpdate);
        }
        public async Task<bool> Delete(string companyCode)
        {
            return await _employeeRepository.Delete(companyCode);
        }       
    }
}
