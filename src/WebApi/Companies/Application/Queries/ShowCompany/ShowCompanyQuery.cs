using System;

namespace WebApi.Companies.Application.Queries.ShowCompany;

public record ShowCompanyQuery
{
    public required string Id { get; set; }
}
