using WebApi.Shared.Abstractions;

namespace WebApi.Surveys.Domain.Errors
{
    public static class SurveySubmissionErrors
    {
        public static readonly Error CreateWithEmptyAnswers = new(
            "SurveySubmissionErrors.CreateWithEmptyAnswers",
            "Por favor, envie pelo menos uma resposta."
        );
    }
}
