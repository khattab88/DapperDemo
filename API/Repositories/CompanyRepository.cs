using API.Contracts;
using API.Data;
using API.Dtos;
using API.Entities;
using Dapper;
using System.Data;

namespace API.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _context;

        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Company> CreateCompany(CompanyCreateDto company)
        {
            var query = "INSERT INTO Companies (Name, Address, Country) VALUES (@Name, @Address, @Country)" +
                "SELECT CAST(SCOPE_IDENTITY() AS int)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);

            using(var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdCompany = new Company()
                {
                    Id = id,
                    Name = company.Name,
                    Address = company.Address,
                    Country = company.Country,
                };

                return createdCompany;
            }
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
