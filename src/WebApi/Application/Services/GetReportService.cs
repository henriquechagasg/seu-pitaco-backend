using Amazon.DynamoDBv2.DataModel;
using WebApi.Domain.Entities;

namespace WebApi.Application.Services;

public class GetReportService(IDynamoDBContext _context)
{
    public async Task<List<Survey>> Execute()
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
