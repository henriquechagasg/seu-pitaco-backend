using WebApi.Customers.Domain.Entities;

namespace WebApi.Surveys.Domain.Entities;

public class SurveyAnswer
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public Guid QuestionId { get; set; }
    public int? NumericValue { get; set; }
    public string? TextValue { get; set; }
    public string? ExtraComment { get; set; }
    public Survey Survey { get; set; } = null!;
    public SurveyQuestion Question { get; set; } = null!;
    public Customer? Customer { get; set; }
    public DateTime CreatedAt { get; set; }
}
