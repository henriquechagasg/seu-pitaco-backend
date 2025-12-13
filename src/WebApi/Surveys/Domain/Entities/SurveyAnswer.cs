using WebApi.Shared.Abstractions;
using WebApi.Shared.Core.Enums;
using WebApi.Surveys.Domain.Errors;

namespace WebApi.Surveys.Domain.Entities;

public record CreateSurveyAnswerInput(
    SurveyQuestion Question,
    QuestionType Type,
    int? NumericValue,
    string? TextValue,
    string? ExtraComment
);

public class SurveyAnswer
{
    public Guid Id { get; set; }
    public Guid SubmissionId { get; set; }
    public Guid QuestionId { get; set; }
    public QuestionType Type { get; set; }
    public int? NumericValue { get; set; }
    public string? TextValue { get; set; }
    public string? ExtraComment { get; set; }
    public SurveyQuestion Question { get; set; } = null!;
    public SurveySubmission Submission { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public static Result<SurveyAnswer> Create(CreateSurveyAnswerInput input)
    {
        var question = input.Question;

        if (input.Type != question.Type)
        {
            return SurveyAnswerErrors.AnswerTypeMismatch;
        }

        if (input.NumericValue.HasValue && !string.IsNullOrEmpty(input.TextValue))
        {
            return SurveyAnswerErrors.InvalidAnswerInput;
        }

        switch (input.Type)
        {
            case QuestionType.CSAT:
                if (!input.NumericValue.HasValue)
                    return SurveyAnswerErrors.MissingNumericValue(input.Type);

                if (input.NumericValue < 1 || input.NumericValue > 5)
                    return SurveyAnswerErrors.NumericValueOutOfRange(input.Type, min: 1, max: 5);

                break;

            case QuestionType.NPS:
                if (!input.NumericValue.HasValue)
                    return SurveyAnswerErrors.MissingNumericValue(input.Type);

                if (input.NumericValue < 0 || input.NumericValue > 10)
                    return SurveyAnswerErrors.NumericValueOutOfRange(input.Type, min: 0, max: 10);

                break;

            case QuestionType.OpenText:
                if (string.IsNullOrEmpty(input.TextValue))
                    return SurveyAnswerErrors.MissingTextValue(input.Type);

                break;

            case QuestionType.SingleChoice:
                if (string.IsNullOrEmpty(input.TextValue))
                    return SurveyAnswerErrors.MissingTextValue(input.Type);

                if (!question.Options!.Select(o => o.Value).Contains(input.TextValue))
                    return SurveyAnswerErrors.InvalidOptionForQuestion(question.Id);

                break;

            default:
                return SurveyAnswerErrors.InvalidQuestionType(input.Type);
        }

        return new SurveyAnswer
        {
            QuestionId = input.Question.Id,
            Type = input.Type,
            NumericValue = input.NumericValue,
            TextValue = input.TextValue,
            ExtraComment = input.ExtraComment,
        };
    }
}
