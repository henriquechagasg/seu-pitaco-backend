using WebApi.Surveys.Application.Dtos;

namespace WebApi.Surveys.Application.Commands.UpdateSurveyQuestionsCommand;

public record UpdateSurveyQuestionsCommand(Guid SurveyId, List<UpdateSurveyQuestionDto> Questions);
