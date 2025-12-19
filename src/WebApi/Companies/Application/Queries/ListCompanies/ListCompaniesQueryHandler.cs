using Microsoft.EntityFrameworkCore;
using WebApi.Companies.Application.Dtos;
using WebApi.Companies.Application.Mapping;
using WebApi.Shared.Abstractions;
using WebApi.Shared.Infrastructure;

namespace WebApi.Companies.Application.Queries.ListCompanies;

public class ListCompaniesQueryHandler(AppDbContext _context)
{
    public async Task<Result<List<CompanyDto>>> Handle()
    {
        var companies = await _context.Companies.Include(c => c.Surveys).ToListAsync();

        return companies.Select(c => c.ToDto()).ToList();
    }
}
