namespace WebApi.Surveys.Domain.Entities;

public class SurveyBuilder
{
    private readonly Survey _survey;

    public SurveyBuilder()
    {
        var random = new Random();
        _survey = new Survey
        {
            Id = Guid.NewGuid().ToString(),
            Company = "seu-pitaco",
            Duration = random.Next(20000, 100000),
            RecaptchaToken = "any-recaptcha-token",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
    }

    public SurveyBuilder WithId(string id)
    {
        _survey.Id = id;
        return this;
    }

    public SurveyBuilder WithCompany(string company)
    {
        _survey.Company = company;
        return this;
    }

    public SurveyBuilder WithDuration(int duration)
    {
        _survey.Duration = duration;
        return this;
    }

    public SurveyBuilder WithRecaptchaToken(string recaptchaToken)
    {
        _survey.RecaptchaToken = recaptchaToken;
        return this;
    }

    public SurveyBuilder WithNpsScore(int npsScore)
    {
        _survey.NpsScore = npsScore;
        return this;
    }

    public SurveyBuilder WithAttendanceRating(int attendanceRating)
    {
        _survey.AttendanceRating = attendanceRating;
        return this;
    }

    public SurveyBuilder WithGeneralComment(string generalComment)
    {
        _survey.GeneralComment = generalComment;
        return this;
    }

    public SurveyBuilder WithRecaptchaScore(float recaptchaScore)
    {
        _survey.RecaptchaScore = recaptchaScore;
        return this;
    }

    public Survey Build()
    {
        return _survey;
    }
}
