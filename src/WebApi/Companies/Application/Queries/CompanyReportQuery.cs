using Amazon.DynamoDBv2.DataModel;
using WebApi.Companies.Application.Dtos;
using WebApi.Surveys.Domain.Entities;

namespace WebApi.Companies.Application.Queries;

public class CompanyReportQuery(IDynamoDBContext _context)
{
    public async Task<CompanyReportResultDto> Handle()
    {
        var surveys = await ScanSurveysAsync();

        var Nps = CalculateNPS(surveys);

        return new CompanyReportResultDto { Count = surveys.Count, Nps = Nps };
    }

    private async Task<List<Survey>> ScanSurveysAsync()
    {
        var search = _context.FromScanAsync<Survey>(
            new Amazon.DynamoDBv2.DocumentModel.ScanOperationConfig() { ConsistentRead = true }
        );
        var searchResponse = await search.GetRemainingAsync();
        return searchResponse;
    }

    public float CalculateNPS(List<Survey> surveys)
    {
        var validSurveys = surveys.Where(s => s.NpsScore.HasValue).ToList();

        int totalResponses = validSurveys.Count;

        if (totalResponses == 0)
            return 0f;

        int promoters = validSurveys.Count(s => s.NpsScore >= 9);

        int detractors = validSurveys.Count(s => s.NpsScore <= 6);

        float nps = (float)(promoters - detractors) / totalResponses * 100f;

        return nps;
    }
}
