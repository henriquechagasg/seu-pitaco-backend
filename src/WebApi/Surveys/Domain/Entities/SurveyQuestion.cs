using WebApi.Shared.Core.Enums;

namespace WebApi.Surveys.Domain.Entities;

public class SurveyQuestion
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Order { get; set; }
    public QuestionType Type { get; set; }
    public bool IsRequired { get; set; }
    public bool AllowComment { get; set; }
    public string? Metadata { get; set; }
    public Survey Survey { get; set; } = null!;
}
