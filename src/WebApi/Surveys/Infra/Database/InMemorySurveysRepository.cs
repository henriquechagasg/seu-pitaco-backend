using WebApi.Surveys.Domain.Repositories;

namespace WebApi.Surveys.Infra.Database;

public class InMemorySurveysRepository : ISurveysRepository
{
    private readonly List<DynamoDbSurvey> _surveys = [];

    public Task<List<DynamoDbSurvey>> FindMany()
    {
        return Task.FromResult(_surveys.ToList());
    }

    public Task<DynamoDbSurvey> Save(DynamoDbSurvey survey)
    {
        var existingSurvey = _surveys.FirstOrDefault(s => s.Id == survey.Id);

        if (existingSurvey != null)
        {
            var index = _surveys.IndexOf(existingSurvey);
            if (index != -1)
            {
                _surveys[index] = survey;
            }
        }
        else
        {
            if (survey.Id == string.Empty)
            {
                survey.Id = Guid.NewGuid().ToString();
            }

            _surveys.Add(survey);
        }

        return Task.FromResult(survey);
    }
}
