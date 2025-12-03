using Amazon.DynamoDBv2.DataModel;
using WebApi.Companies.Application.Dtos;
using WebApi.Surveys.Domain.Entities;
using WebApi.Surveys.Domain.Repositories;

namespace WebApi.Companies.Application.Queries;

public class CompanyReportQuery(ISurveysRepository _surveysRepository)
{
    public async Task<CompanyReportResultDto> Handle()
    {
        var surveys = await _surveysRepository.FindMany();

        var Nps = CalculateNPS(surveys);

        return new CompanyReportResultDto { Count = surveys.Count, Nps = Nps };
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
