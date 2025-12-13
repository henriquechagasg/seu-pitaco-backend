using WebApi.Shared.Abstractions;
using WebApi.Shared.Core.Enums;

namespace WebApi.Surveys.Domain.Errors
{
    public static class SurveyAnswerErrors
    {
        public static readonly Error AnswerTypeMismatch = new(
            "SurveyAnswerErrors.AnswerTypeMismatch",
            "O tipo de resposta fornecido não corresponde ao tipo de pergunta esperado."
        );

        public static readonly Error InvalidAnswerInput = new(
            "SurveyAnswerErrors.InvalidAnswerInput",
            "Apenas um campo de valor principal (Numérico ou Texto) deve ser preenchido para a resposta."
        );

        public static Error InvalidQuestionType(QuestionType type) =>
            new(
                "SurveyAnswerErrors.InvalidQuestionType",
                $"O tipo de pergunta '{type}' é desconhecido ou não é suportado."
            );

        public static Error MissingNumericValue(QuestionType type) =>
            new(
                "SurveyAnswerErrors.MissingNumericValue",
                $"Para o tipo de pergunta {type}, o valor numérico (nota) é obrigatório e não pode estar vazio."
            );

        public static Error NumericValueOutOfRange(QuestionType type, int min, int max) =>
            new(
                "NumericValueOutOfRange",
                $"Valor inválido para '{type}'. Intervalo permitido: {min} a {max}."
            );

        public static Error MissingTextValue(QuestionType type) =>
            new(
                "SurveyAnswerErrors.MissingTextValue",
                $"Para o tipo de pergunta {type}, a resposta de texto é obrigatória e não pode estar vazia."
            );

        public static Error InvalidOptionForQuestion(Guid questionId) =>
            new(
                "SurveyAnswerErrors.InvalidOptionForQuestion",
                $"A opção escolhida não é uma opção válida para a pergunta de id {questionId}."
            );
    }
}
