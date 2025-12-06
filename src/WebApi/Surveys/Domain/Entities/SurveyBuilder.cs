using WebApi.Surveys.Infra.Database;

namespace WebApi.Surveys.Domain.Entities;

public class SurveyBuilder
{
    private readonly DynamoDbSurvey _survey;
    private readonly Random random = Random.Shared;

    public SurveyBuilder()
    {
        _survey = new DynamoDbSurvey
        {
            Id = Guid.NewGuid().ToString(),
            Company = "seu-pitaco",
            Duration = random.Next(20000, 100000),
            RecaptchaToken = "any-recaptcha-token",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
    }

    public SurveyBuilder WithNpsScore(int score)
    {
        _survey.NpsScore = score;
        return this;
    }

    public SurveyBuilder WithPromoterScore()
    {
        _survey.NpsScore = random.Next(9, 10);
        return this;
    }

    public SurveyBuilder WithNeutralScore()
    {
        _survey.NpsScore = random.Next(7, 8);
        return this;
    }

    public SurveyBuilder WithDetractorScore()
    {
        _survey.NpsScore = random.Next(0, 6);
        return this;
    }

    public DynamoDbSurvey Build()
    {
        return _survey;
    }
}
