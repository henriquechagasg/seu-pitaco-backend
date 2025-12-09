using WebApi.Surveys.Application.Dtos;
using WebApi.Surveys.Domain.Entities;

namespace WebApi.Surveys.Application.Mappings;

public static class SurveyMapping
{
    public static SurveyDto ToDto(this Survey entity) =>
        new(
            entity.Id,
            entity.Title,
            entity.IsDefault,
            entity.CreatedAt,
            [.. entity.Questions.Select(q => q.ToDto())]
        );

    public static SurveyQuestionDto ToDto(this SurveyQuestion entity) =>
        new(
            entity.Id,
            entity.SurveyId,
            entity.Order,
            entity.Title,
            entity.Type,
            entity.IsRequired,
            entity.AllowComment,
            entity.Metadata
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
            AllowComment = dto.AllowComment,
            Metadata = dto.Metadata,
        };

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
