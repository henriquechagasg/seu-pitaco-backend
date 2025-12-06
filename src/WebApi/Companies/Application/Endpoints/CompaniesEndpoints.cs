using Microsoft.AspNetCore.Http.HttpResults;
using WebApi.Companies.Application.Commands.CreateCompany;
using WebApi.Companies.Application.Dtos;
using WebApi.Companies.Application.Queries.ListCompanies;
using WebApi.Companies.Application.Queries.ShowCompany;
using WebApi.Companies.Application.Queries.ShowCompanyReport;
using WebApi.Companies.Domain.Entities;
using WebApi.Shared.Abstractions;

namespace WebApi.Companies.Application.Endpoints
{
    public static class CompaniesEndpoints
    {
        public static void MapCompaniesEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/companies");

            group.MapGet("/", ListCompanies).WithName("ListCompanies").WithOpenApi();

            group.MapGet("/{id}", ShowCompany).WithName("ShowCompany").WithOpenApi();

            group.MapPost("/", CreateCompany).WithName("CreateCompany").WithOpenApi();

            group.MapGet("/report", ShowCompanyReport).WithName("ShowCompanyReport").WithOpenApi();
        }

        public static async Task<CompanyReportResultDto?> ShowCompanyReport(
            ShowCompanyReportQuery handler
        )
        {
            var item = await handler.Handle();
            return item;
        }

        public static async Task<
            Results<CreatedAtRoute<CompanyDto>, BadRequest<Error>>
        > CreateCompany(CreateCompanyCommand command, CreateCompanyCommandHandler handler)
        {
            var result = await handler.Handle(command);

            if (result.IsFailure)
            {
                return TypedResults.BadRequest(result.Error);
            }

            var value = result.Value;
            return TypedResults.CreatedAtRoute(
                routeName: "ShowCompany",
                routeValues: new { id = value.Id },
                value: value
            );
        }

        public static async Task<Ok<List<Company>>> ListCompanies(ListCompaniesQueryHandler handler)
        {
            var result = await handler.Handle();
            return TypedResults.Ok(result.Value);
        }

        public static async Task<Results<Ok<Company>, BadRequest<Error>>> ShowCompany(
            string Id,
            ShowCompanyQueryHandler handler
        )
        {
            var query = new ShowCompanyQuery { Id = Id };

            var result = await handler.Handle(query);

            if (result.IsFailure)
            {
                return TypedResults.BadRequest(result.Error);
            }

            var value = result.Value;
            return TypedResults.Ok(value);
        }
    }
}
