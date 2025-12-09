using WebApi.Surveys.Application.Dtos;

namespace WebApi.Surveys.Application.Commands.UpdateSurveyQuestions;

public record UpdateSurveyQuestionsCommand(Guid SurveyId, List<UpdateSurveyQuestionDto> Questions);
