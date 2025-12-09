using WebApi.Shared.Core.Enums;

namespace WebApi.Surveys.Application.Dtos;

public record UpdateSurveyQuestionDto(
    string Title,
    QuestionType Type,
    bool IsRequired,
    bool AllowComment,
    string? Metadata
);
