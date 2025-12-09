using WebApi.Shared.Core.Enums;
using WebApi.Surveys.Domain.Entities;

namespace WebApi.Surveys.Application.Dtos;

public record UpdateSurveyQuestionDto(
    string Title,
    QuestionType Type,
    bool IsRequired,
    bool AllowComment,
    List<SurveyQuestionMetadata>? Metadata
);
