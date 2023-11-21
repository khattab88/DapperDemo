using API.Contracts;
using API.Data;
using API.Entities;
using Dapper;

namespace API.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _context;

        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var query = "SELECT Id, Name AS CompanyName, Address, Country FROM Companies";

            using(var connection = _context.CreateConnection()) 
            {
                var companies = await connection.QueryAsync<Company>(query);

                return companies.ToList();
            }
        }

        public async Task<Company> GetCompany(int id)
        {
            var query = $"SELECT Id, Name AS CompanyName, Address, Country FROM Companies WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new { id });

                return company;
            }
        }
    }
}
