using WebApi.Surveys.Application.Dtos;

namespace WebApi.Surveys.Application.Commands.CreateSurveySubmission;

public record CreateSurveySubmissionCommand(Guid SurveyId, List<CreateSurveyAnswerDto> Answers);
