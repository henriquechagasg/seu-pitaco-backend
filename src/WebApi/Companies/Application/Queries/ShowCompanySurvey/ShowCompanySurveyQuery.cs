namespace WebApi.Companies.Application.Queries.ShowCompanySurvey;

public record ShowCompanySurveyQuery
{
    public required string Slug { get; set; }
}
