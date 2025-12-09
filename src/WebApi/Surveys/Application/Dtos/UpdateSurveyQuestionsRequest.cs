namespace WebApi.Surveys.Application.Dtos
{
    public record UpdateSurveyQuestionsRequest(List<UpdateSurveyQuestionDto> Questions);
}
