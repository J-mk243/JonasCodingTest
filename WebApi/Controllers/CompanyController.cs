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
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        // GET api/<controller>
        [HttpGet]
        public async Task<IEnumerable<CompanyDto>> GetAllAsync()
        {
            var items = await _companyService.GetAllCompanies();
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
        }

        // GET api/<controller>/5
        [HttpGet]
        public async Task<CompanyDto> GetAsync(string companyCode)
        {
            var item = await _companyService.GetCompanyByCode(companyCode);
            return _mapper.Map<CompanyDto>(item);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IHttpActionResult> PostAsync(CompanyDto companyDto)
        {
            if (companyDto != null)
            {
                var itemCompanyInfo = _mapper.Map<CompanyInfo>(companyDto);
                await _companyService.Post(itemCompanyInfo);

                return Ok("Company created successfully.");
            }
            return InternalServerError(new Exception("Failed create the company."));
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IHttpActionResult> PutAsync(string companyCode, [FromBody] CompanyDto companyDto)
        {
            if (companyDto == null || string.IsNullOrEmpty(companyCode))
            {
                return BadRequest("Invalid data or company code.");
            }

            if (await _companyService.GetCompanyByCode(companyCode) != null)
            {
                var itemCompanyInfo = _mapper.Map<CompanyInfo>(companyDto);
                await _companyService.Put(itemCompanyInfo);

                return Ok("Company updated successfully.");
            }
            return InternalServerError(new Exception("Failed to update the company."));
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(string companyCode)
        {
            if ( await _companyService.GetCompanyByCode(companyCode) == null)
            {
                return NotFound();
            }

            if (await _companyService.Delete(companyCode))
            {
                return Ok("Company deleted successfully.");
            }
            return InternalServerError(new Exception("Failed to delete the company."));
        }
    }
}