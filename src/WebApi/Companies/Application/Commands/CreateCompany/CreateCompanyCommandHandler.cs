using Microsoft.EntityFrameworkCore;
using WebApi.Companies.Application.Dtos;
using WebApi.Companies.Application.Mapping;
using WebApi.Companies.Domain.Entities;
using WebApi.Shared.Abstractions;
using WebApi.Shared.Infrastructure;
using WebApi.Surveys.Domain.Entities;

namespace WebApi.Companies.Application.Commands.CreateCompany;

public class CreateCompanyCommandHandler(AppDbContext _context)
{
    public async Task<Result<CompanyDto>> Handle(CreateCompanyCommand command)
    {
        var companyWithSlug = await _context.Companies.FirstOrDefaultAsync(c =>
            c.Slug == command.Slug
        );

        if (companyWithSlug != null)
        {
            return new Error(
                "CompanySlugAlreadyExists",
                "O slug enviado já está cadastrado por outra empresa."
            );
        }

        var company = new Company { Name = command.Name, Slug = command.Slug };
        company.Surveys.Add(new Survey { IsDefault = true });

        _context.Companies.Add(company);

        await _context.SaveChangesAsync();

        return company.ToDto();
    }
}
