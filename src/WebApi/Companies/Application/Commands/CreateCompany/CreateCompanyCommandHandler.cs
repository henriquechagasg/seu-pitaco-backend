using WebApi.Companies.Domain.Entities;
using WebApi.Shared.Infrastructure;

namespace WebApi.Companies.Application.Commands.CreateCompany;

public class CreateCompanyCommandHandler(AppDbContext _context)
{
    public async Task<Company> Handle(CreateCompanyCommand command)
    {
        var company = new Company { Name = command.Name, Slug = command.Slug };

        _context.Companies.Add(company);

        await _context.SaveChangesAsync();

        return company;
    }
}
