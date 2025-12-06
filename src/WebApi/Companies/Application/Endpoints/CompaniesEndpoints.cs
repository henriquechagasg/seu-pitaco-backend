using WebApi.Companies.Application.Commands.CreateCompany;
using WebApi.Companies.Application.Dtos;
using WebApi.Companies.Application.Queries;

namespace WebApi.Companies.Application.Endpoints
{
    public static class CompaniesEndpoints
    {
        public static void MapCompaniesEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/companies");

            group.MapPost("/", CreateCompany).WithName("CreateCompany").WithOpenApi();

            group
                .MapGet("/report", GetCompanyReport)
                .Produces<CompanyReportResultDto>(StatusCodes.Status200OK)
                .WithName("GetCompanyReport")
                .WithOpenApi();
        }

        public static async Task<IResult> GetCompanyReport(CompanyReportQuery CompanyReportQuery)
        {
            var item = await CompanyReportQuery.Handle();
            return Results.Ok(item);
        }

        public static async Task<IResult> CreateCompany(
            CreateCompanyCommand command,
            CreateCompanyCommandHandler createCompanyCommandHandler
        )
        {
            var item = await createCompanyCommandHandler.Handle(command);
            return Results.Created($"/api/companies/{item.Id}", item);
        }
    }
}
