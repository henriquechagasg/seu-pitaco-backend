using WebApi.Shared.Abstractions;

namespace WebApi.Surveys.Domain.Entities;

public class SurveySubmission
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public string? Metadata { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Survey Survey { get; set; } = null!;
    public ICollection<SurveyAnswer> Answers { get; set; } = [];

    public static Result<SurveySubmission> Create(CreateSurveySubmissionInput input)
    {
        var answers = input.Answers;

        if (answers.Count == 0)
        {
            return new Error(
                "SubmissionAnswersEmptyError",
                "Por favor, envie pelo menos uma resposta."
            );
        }

        return new SurveySubmission { SurveyId = input.SurveyId, Answers = answers };
    }
}

public record CreateSurveySubmissionInput(Guid SurveyId, List<SurveyAnswer> Answers);
