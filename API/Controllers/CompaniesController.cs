using API.Contracts;
using API.Dtos;
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
    }
}
