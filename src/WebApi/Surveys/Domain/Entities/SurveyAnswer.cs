using WebApi.Customers.Domain.Entities;

namespace WebApi.Surveys.Domain.Entities;

public class SurveyAnswer
{
    public Guid Id { get; set; }
    public Guid SubmissionId { get; set; }
    public Guid QuestionId { get; set; }
    public int? NumericValue { get; set; }
    public string? TextValue { get; set; }
    public string? ExtraComment { get; set; }
    public SurveyQuestion Question { get; set; } = null!;
    public SurveySubmission Submission { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
