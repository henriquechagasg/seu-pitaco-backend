using WebApi.Companies.Application.Queries;

namespace WebApi.Companies.Application.Endpoints
{
    public static class CompaniesEndpoints
    {
        public static void MapCompaniesEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/companies");
            group.MapGet("/report", GetCompanyReport).WithName("GetCompanyReport").WithOpenApi();
        }

        public static async Task<IResult> GetCompanyReport(CompanyReportQuery CompanyReportQuery)
        {
            var item = await CompanyReportQuery.Handle();
            return Results.Ok(item);
        }
    }
}
