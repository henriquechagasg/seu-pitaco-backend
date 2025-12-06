using Microsoft.EntityFrameworkCore;
using WebApi.Companies.Domain.Entities;
using WebApi.Shared.Abstractions;
using WebApi.Shared.Infrastructure;

namespace WebApi.Companies.Application.Queries.ListCompanies;

public class ListCompaniesQueryHandler(AppDbContext _context)
{
    public async Task<Result<List<Company>>> Handle()
    {
        var companies = await _context.Companies.ToListAsync();
        return companies;
    }
}
