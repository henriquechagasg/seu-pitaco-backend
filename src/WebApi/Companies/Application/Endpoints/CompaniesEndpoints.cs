using WebApi.Companies.Application.Commands.CreateCompany;
using WebApi.Companies.Application.Dtos;
using WebApi.Companies.Application.Queries.ShowCompanyReport;

namespace WebApi.Companies.Application.Endpoints
{
    public static class CompaniesEndpoints
    {
        public static void MapCompaniesEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/companies");

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
            var item = await handler.Handle(command);
            return Results.Created($"/api/companies/{item.Id}", item);
        }
    }
}
