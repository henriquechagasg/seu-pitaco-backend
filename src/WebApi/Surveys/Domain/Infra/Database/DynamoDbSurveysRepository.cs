using Amazon.DynamoDBv2.DataModel;
using WebApi.Surveys.Domain.Entities;
using WebApi.Surveys.Domain.Repositories;

namespace WebApi.Surveys.Domain.Infra.Database;

public class DynamoDbSurveysRepository(IDynamoDBContext _context) : ISurveysRepository
{
    public async Task<List<Survey>> FindMany()
    {
        var search = _context.FromScanAsync<Survey>(
            new Amazon.DynamoDBv2.DocumentModel.ScanOperationConfig() { ConsistentRead = true }
        );

        var searchResponse = await search.GetRemainingAsync();
        return searchResponse;
    }
}
