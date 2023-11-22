using API.Dtos;
using API.Entities;

namespace API.Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompanies();
        Task<Company> GetCompany(int id);
        Task<Company> CreateCompany(CompanyCreateDto company);
    }
}
