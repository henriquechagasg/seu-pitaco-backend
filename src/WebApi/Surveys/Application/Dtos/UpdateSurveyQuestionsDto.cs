using WebApi.Shared.Core.Enums;
using WebApi.Surveys.Domain.Entities;

namespace WebApi.Surveys.Application.Dtos;

public record UpdateSurveyQuestionDto(
    string Title,
    QuestionType Type,
    bool IsRequired,
    List<SurveyQuestionOption>? Options
);

public static class UpdateSurveyQuestionDtoMapping
{
    public static SurveyQuestion ToEntity(this UpdateSurveyQuestionDto dto) =>
        new()
        {
            Title = dto.Title,
            Type = dto.Type,
            IsRequired = dto.IsRequired,
            Options = dto.Options,
        };
}
