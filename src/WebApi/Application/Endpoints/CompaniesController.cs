using WebApi.Application.Services;

namespace WebApi.Application.Endpoints
{
    public static class CompaniesEndpoints
    {
        public static void MapCompaniesEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/companies");
            group.MapGet("/report", GetCompanyReport).WithName("GetCompanyReport").WithOpenApi();
        }

        public static async Task<IResult> GetCompanyReport(GetReportService getReportService)
        {
            var item = await getReportService.Execute();
            return Results.Ok(item);
        }
    }
}
