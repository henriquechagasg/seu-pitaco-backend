namespace WebApi.Surveys.Application.Dtos;

public record CreateSurveySubmissionRequest(List<CreateSurveyAnswerDto> Answers);
