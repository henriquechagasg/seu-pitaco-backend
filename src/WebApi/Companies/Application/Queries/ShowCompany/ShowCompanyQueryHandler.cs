using WebApi.Companies.Domain.Entities;
using WebApi.Shared.Abstractions;
using WebApi.Shared.Infrastructure;

namespace WebApi.Companies.Application.Queries.ShowCompany;

public class ShowCompanyQueryHandler(AppDbContext _context)
{
    public async Task<Result<Company>> Handle(ShowCompanyQuery query)
    {
        if (!Guid.TryParse(query.Id, out var companyId))
        {
            return new Error("InvalidId", "Id inválido.");
        }

        var company = await _context.Companies.FindAsync(companyId);

        if (company == null)
        {
            return new Error("NotFoundError", "Empresa não encontrada.");
        }

        return company;
    }
}
