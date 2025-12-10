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
    List<SurveyQuestionOption>? Options
);

public static class SurveyQuestionDtoMapping
{
    public static SurveyQuestionDto ToDto(this SurveyQuestion entity) =>
        new(
            entity.Id,
            entity.SurveyId,
            entity.Order,
            entity.Title,
            entity.Type,
            entity.IsRequired,
            entity.Options
        );

    public static SurveyQuestion ToEntity(this SurveyQuestionDto dto) =>
        new()
        {
            Id = dto.Id,
            SurveyId = dto.SurveyId,
            Order = dto.Order,
            Title = dto.Title,
            Type = dto.Type,
            IsRequired = dto.IsRequired,
            Options = dto.Options,
        };
}
