using Microsoft.EntityFrameworkCore;
using WebApi.Companies.Domain.Entities;
using WebApi.Shared.Abstractions;
using WebApi.Shared.Infrastructure;

namespace WebApi.Companies.Application.Commands.CreateCompany;

public class CreateCompanyCommandHandler(AppDbContext _context)
{
    public async Task<Result<Company>> Handle(CreateCompanyCommand command)
    {
        var companyWithSlug = _context.Companies.FirstOrDefaultAsync(c => c.Slug == command.Slug);

        if (companyWithSlug != null)
        {
            return new Error(
                "CompanySlugAlreadyExists",
                "O slug enviado já está cadastrado por outra empresa."
            );
        }

        var company = new Company { Name = command.Name, Slug = command.Slug };

        _context.Companies.Add(company);

        await _context.SaveChangesAsync();

        return company;
    }
}
