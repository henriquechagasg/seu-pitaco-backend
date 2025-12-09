namespace WebApi.Surveys.Domain.Entities;

public class SurveySubmission
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public string? Metadata { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Survey Survey { get; set; } = null!;
    public List<SurveyAnswer> Answers { get; set; } = null!;
}
