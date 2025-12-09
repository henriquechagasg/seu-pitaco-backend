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

public static class UpdateSurveyQuestionDtoMapping
{
    public static SurveyQuestion ToEntity(this UpdateSurveyQuestionDto dto) =>
        new()
        {
            Title = dto.Title,
            Type = dto.Type,
            IsRequired = dto.IsRequired,
            AllowComment = dto.AllowComment,
            Metadata = dto.Metadata,
        };
}
