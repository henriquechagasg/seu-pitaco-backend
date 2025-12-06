using WebApi.Companies.Application.Dtos;
using WebApi.Surveys.Domain.Repositories;
using WebApi.Surveys.Infra.Database;

namespace WebApi.Companies.Application.Queries;

public class CompanyReportQuery(ISurveysRepository _surveysRepository)
{
    public async Task<CompanyReportResultDto?> Handle()
    {
        var surveys = await _surveysRepository.FindMany();

        var companyName = "carretasmillenium";
        var companySurveys = surveys.Where(c => c.Company == companyName);

        if (surveys.Count == 0)
            return null;

        var npsSummary = GetNpsSummary(surveys);

        return new CompanyReportResultDto { Count = surveys.Count, NpsSummary = npsSummary };
    }

    private static NpsSummary GetNpsSummary(List<DynamoDbSurvey> surveys)
    {
        int totalResponses = surveys.Count;

        int promotersCount = surveys.Count(s => s.NpsScore >= 9);

        int detractorsCount = surveys.Count(s => s.NpsScore <= 6);

        int neutralsCount = surveys.Count(s => s.NpsScore >= 7 && s.NpsScore <= 8);

        float score = (float)(promotersCount - detractorsCount) / totalResponses * 100f;

        var ratingDistribution = GenerateRatingDistribution(surveys);

        return new NpsSummary
        {
            Score = score,
            PromotersCount = promotersCount,
            DetractorsCount = detractorsCount,
            NeutralsCount = neutralsCount,
            RatingDistribution = ratingDistribution,
        };
    }

    private static Dictionary<string, int> GenerateRatingDistribution(List<DynamoDbSurvey> surveys)
    {
        var ratingDistribution = new Dictionary<string, int>();

        for (int i = 0; i <= 10; i++)
        {
            ratingDistribution.Add(i.ToString(), 0);
        }

        var groupedCounts = surveys
            .GroupBy(s => s.NpsScore!.Value)
            .ToDictionary(g => g.Key, g => g.Count());

        foreach (var kvp in groupedCounts)
        {
            if (kvp.Key >= 0 && kvp.Key <= 10)
            {
                ratingDistribution[kvp.Key.ToString()] = kvp.Value;
            }
        }

        return ratingDistribution;
    }
}
