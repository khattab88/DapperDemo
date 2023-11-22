using API.Dtos;
using API.Entities;

namespace API.Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompanies();
        Task<Company> GetCompany(int id);
        Task<Company> CreateCompany(CompanyCreateDto company);
        Task UpdateCompany(int id, CompanyUpdateDto company);
        Task DeleteCompany(int id);
        Task<Company> GetCompanyByEmployeeId(int empId);
        Task<Company> GetMultipleResults(int id);
        Task<List<Company>> MultipleMapping();
        Task CreateMultipleCompanies(List<CompanyCreateDto> companies);
    }
}
