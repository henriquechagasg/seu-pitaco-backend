using WebApi.Companies.Application.Commands.CreateCompany;
using WebApi.Companies.Application.Dtos;
using WebApi.Companies.Application.Queries.ListCompanies;
using WebApi.Companies.Application.Queries.ShowCompany;
using WebApi.Companies.Application.Queries.ShowCompanyReport;

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

        public static async Task<IResult> CreateCompany(
            CreateCompanyCommand command,
            CreateCompanyCommandHandler handler
        )
        {
            var result = await handler.Handle(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            var value = result.Value;
            return Results.Created($"/api/companies/{value.Id}", value);
        }

        public static async Task<IResult> ListCompanies(ListCompaniesQueryHandler handler)
        {
            var result = await handler.Handle();
            var value = result.Value;
            return Results.Ok(value);
        }

        public static async Task<IResult> ShowCompany(string Id, ShowCompanyQueryHandler handler)
        {
            var query = new ShowCompanyQuery { Id = Id };

            var result = await handler.Handle(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            var value = result.Value;
            return Results.Ok(value);
        }
    }
}
