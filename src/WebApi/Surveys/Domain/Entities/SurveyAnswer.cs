using WebApi.Shared.Abstractions;
using WebApi.Shared.Core.Enums;

namespace WebApi.Surveys.Domain.Entities;

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
            return new Error(
                "QuestionTypeMismatchError",
                "Tipo da resposta não equivale ao tipo pergunta."
            );
        }

        if (input.NumericValue.HasValue && !string.IsNullOrEmpty(input.TextValue))
        {
            return new Error(
                "InvalidAnswerInput",
                "Apenas um campo de valor (Numérico ou Texto) deve ser preenchido para a resposta principal."
            );
        }

        switch (input.Type)
        {
            case QuestionType.CSAT:
            case QuestionType.NPS:
                if (!input.NumericValue.HasValue)
                {
                    return new Error(
                        "MissingNumericValue",
                        $"Para o tipo de pergunta {input.Type}, o valor numérico (nota) é obrigatório."
                    );
                }
                break;

            case QuestionType.OpenText:
                if (string.IsNullOrEmpty(input.TextValue))
                {
                    return new Error(
                        "MissingTextValue",
                        "Para o tipo de pergunta OpenText, a resposta de texto é obrigatória."
                    );
                }
                break;

            case QuestionType.SingleChoice:
                if (string.IsNullOrEmpty(input.TextValue))
                {
                    return new Error(
                        "MissingTextValue",
                        "Para o tipo de pergunta OpenText, a resposta de texto é obrigatória."
                    );
                }

                if (question.Options!.Select(o => o.Value).Contains(input.TextValue))
                {
                    return new Error(
                        "InvalidOptionForQuestion",
                        $"A opção escolhida é inválida para a pergunta {question.Id}"
                    );
                }
                break;

            default:
                return new Error(
                    "InvalidQuestionType",
                    $"Tipo de pergunta '{input.Type}' desconhecido."
                );
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

public record CreateSurveyAnswerInput(
    SurveyQuestion Question,
    QuestionType Type,
    int? NumericValue,
    string? TextValue,
    string? ExtraComment
);
