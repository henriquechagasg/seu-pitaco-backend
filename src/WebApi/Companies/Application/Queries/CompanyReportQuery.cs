using Amazon.DynamoDBv2.DataModel;
using WebApi.Surveys.Domain.Entities;

namespace WebApi.Companies.Application.Queries;

public class CompanyReportQuery(IDynamoDBContext _context)
{
    public async Task<List<Survey>> Handle()
    {
        var surveys = await ScanSurveysAsync();
        return surveys;
    }

    private async Task<List<Survey>> ScanSurveysAsync()
    {
        var search = _context.FromScanAsync<Survey>(
            new Amazon.DynamoDBv2.DocumentModel.ScanOperationConfig() { ConsistentRead = true }
        );
        var searchResponse = await search.GetRemainingAsync();
        return searchResponse;
    }
}
