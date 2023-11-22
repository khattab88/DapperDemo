using API.Contracts;
using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepo;

        public CompaniesController(ICompanyRepository companyRepo)
        {
            _companyRepo = companyRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyRepo.GetCompanies();

            return Ok(companies);
        }

        [HttpGet("{id}", Name ="GetCompanyById")]
        public async Task<IActionResult> GetCompany(int id)
        {
            var company = await _companyRepo.GetCompany(id);

            if(company == null) 
            {
                return NotFound();
            }

            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCOmpany([FromBody] CompanyCreateDto company)
        {
            var created = await _companyRepo.CreateCompany(company);

            return CreatedAtRoute("GetCompanyById", new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyUpdateDto company)
        {
            var existing = _companyRepo.GetCompany(id);

            if (existing is null)
                return NotFound();

            await _companyRepo.UpdateCompany(id, company);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var existing = _companyRepo.GetCompany(id);

            if (existing is null)
                return NotFound();

            await _companyRepo.DeleteCompany(id);

            return NoContent();
        }

        [HttpGet("ByEmployeeId/{empId}")]
        public async Task<IActionResult> GetCompanyByEmployeeId(int empId) 
        {
            var company = await _companyRepo.GetCompanyByEmployeeId(empId);

            if (company is null)
                return NotFound();

            return Ok(company);
        }

        [HttpGet("{id}/WithEmployees")]
        public async Task<IActionResult> GetCompnyWithEmployees(int id)
        {
            var company = await _companyRepo.GetMultipleResults(id);

            if(company is null)
                return NotFound();

            return Ok(company);
        }
    }
}
