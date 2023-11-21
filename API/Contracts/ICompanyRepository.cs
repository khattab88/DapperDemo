using API.Entities;

namespace API.Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompanies();
    }
}
