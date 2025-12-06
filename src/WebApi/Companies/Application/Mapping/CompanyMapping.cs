using WebApi.Companies.Application.Dtos;
using WebApi.Companies.Domain.Entities;
using WebApi.Surveys.Application.Mappings;

namespace WebApi.Companies.Application.Mapping;

public static class CompanyMapping
{
    public static CompanyDto ToDto(this Company company) =>
        new(
            company.Id,
            company.Name,
            company.Slug,
            company.CreatedAt,
            company.UpdatedAt,
            [.. company.Surveys.Select(s => s.ToDto())]
        );
}
