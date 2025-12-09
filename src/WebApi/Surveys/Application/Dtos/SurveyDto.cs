using WebApi.Surveys.Domain.Entities;

namespace WebApi.Surveys.Application.Dtos;

public record SurveyDto(
    Guid Id,
    string? Title,
    bool IsDefault,
    DateTime CreatedAt,
    List<SurveyQuestionDto> Questions
);

public static class SurveyDtoMapping
{
    public static SurveyDto ToDto(this Survey entity) =>
        new(
            entity.Id,
            entity.Title,
            entity.IsDefault,
            entity.CreatedAt,
            [.. entity.Questions.Select(q => q.ToDto())]
        );
}
