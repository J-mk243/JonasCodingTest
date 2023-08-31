using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _companyService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        // GET api/<controller>
        [HttpGet]
        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var items = await _companyService.GetAllEmployees();
            return _mapper.Map<IEnumerable<EmployeeDto>>(items);
        }

        // GET api/<controller>/5
        [HttpGet]
        public async Task<EmployeeDto> GetAsync(string companyCode)
        {
            var item = await _companyService.GetEmployeeByCode(companyCode);
            return _mapper.Map<EmployeeDto>(item);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IHttpActionResult> PostAsync(EmployeeDto employeeDto)
        {
            if (employeeDto != null)
            {
                var itemCompanyInfo = _mapper.Map<EmployeeInfo>(employeeDto);
                await _companyService.Post(itemCompanyInfo);

                return Ok("Employee created successfully.");
            }
            return InternalServerError(new Exception("Failed create the Employee."));
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IHttpActionResult> PutAsync(string companyCode, [FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null || string.IsNullOrEmpty(companyCode))
            {
                return BadRequest("Invalid data or Employee code.");
            }

            if (await _companyService.GetEmployeeByCode(companyCode) != null)
            {
                var itemCompanyInfo = _mapper.Map<EmployeeInfo>(employeeDto);
                await _companyService.Put(itemCompanyInfo);

                return Ok("Employee updated successfully.");
            }
            return InternalServerError(new Exception("Failed to update the company."));
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(string companyCode)
        {
            if ( await _companyService.GetEmployeeByCode(companyCode) == null)
            {
                return NotFound();
            }

            if (await _companyService.Delete(companyCode))
            {
                return Ok("Employee deleted successfully.");
            }
            return InternalServerError(new Exception("Failed to delete the company."));
        }
    }
}