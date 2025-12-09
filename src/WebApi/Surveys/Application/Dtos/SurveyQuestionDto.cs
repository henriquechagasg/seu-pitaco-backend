using WebApi.Shared.Core.Enums;

namespace WebApi.Surveys.Application.Dtos;

public record SurveyQuestionDto(
    Guid Id,
    Guid SurveyId,
    int Order,
    string Title,
    QuestionType Type,
    bool IsRequired,
    bool AllowComment,
    string? Metadata
);
