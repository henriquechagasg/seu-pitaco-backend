using Amazon.DynamoDBv2.DataModel;
using WebApi.Surveys.Domain.Repositories;

namespace WebApi.Surveys.Infra.Database;

public class DynamoDbSurveysRepository(IDynamoDBContext _context) : ISurveysRepository
{
    public async Task<List<DynamoDbSurvey>> FindMany()
    {
        var search = _context.FromScanAsync<DynamoDbSurvey>(
            new Amazon.DynamoDBv2.DocumentModel.ScanOperationConfig() { ConsistentRead = true }
        );

        var searchResponse = await search.GetRemainingAsync();
        return searchResponse;
    }

    public Task<DynamoDbSurvey> Save(DynamoDbSurvey survey)
    {
        throw new NotImplementedException();
    }
}
