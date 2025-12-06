using WebApi.Surveys.Infra.Database;

namespace WebApi.Surveys.Domain.Repositories;

public interface ISurveysRepository
{
    Task<List<DynamoDbSurvey>> FindMany();
    Task<DynamoDbSurvey> Save(DynamoDbSurvey survey);
}
