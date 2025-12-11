using WebApi.Companies.Domain.Entities;
using WebApi.Shared.Abstractions;
using WebApi.Shared.Core.Enums;
using WebApi.Surveys.Domain.Errors;

namespace WebApi.Surveys.Domain.Entities;

public class Survey
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsDefault { get; set; }
    public string? Metadata { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public Company Company { get; set; } = null!;

    public ICollection<SurveySubmission> Submissions { get; set; } = [];
    public ICollection<SurveyQuestion> Questions { get; set; } = [];

    public Result<List<SurveyQuestion>> UpdateQuestions(List<SurveyQuestion> questions)
    {
        if (Submissions.Count > 0)
        {
            return SurveyErrors.ActionForbidden("A pesquisa contém respostas.");
        }

        int i = 0;
        foreach (var q in questions)
        {
            if (q.Type == QuestionType.SingleChoice && q.Options is null)
            {
                return SurveyErrors.EmptyOptions(q.Id.ToString());
            }

            q.Order = i++;
        }

        Questions = questions;

        return Questions.ToList();
    }
}
