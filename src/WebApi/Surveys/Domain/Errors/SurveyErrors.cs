using WebApi.Shared.Abstractions;

namespace WebApi.Surveys.Domain.Errors
{
    public static class SurveyErrors
    {
        public static Error ActionForbidden(string reason) =>
            new("SurveyErrors.ActionForbidden", $"Ação não permitida: {reason}");

        public static Error EmptyOptions(string questionId) =>
            new(
                "SurveyErrors.EmptyOptions",
                $"As opções não podem estar vazias para pergunta ${questionId}"
            );
    }
}
