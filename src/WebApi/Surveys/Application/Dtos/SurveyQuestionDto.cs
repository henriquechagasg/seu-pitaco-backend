using WebApi.Shared.Core.Enums;
using WebApi.Surveys.Domain.Entities;

namespace WebApi.Surveys.Application.Dtos;

public record SurveyQuestionDto(
    Guid Id,
    Guid SurveyId,
    int Order,
    string Title,
    QuestionType Type,
    bool IsRequired,
    bool AllowComment,
    List<SurveyQuestionMetadata>? Metadata
);
