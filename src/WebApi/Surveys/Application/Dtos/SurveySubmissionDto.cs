using WebApi.Surveys.Domain.Entities;

namespace WebApi.Surveys.Application.Dtos;

public record SurveySubmissionDto(Guid Id, Guid SurveyId, string? Metadata, DateTime CreatedAt);

public static class SurveySubmissionDtoMapping
{
    public static SurveySubmissionDto ToDto(this SurveySubmission entity) =>
        new(entity.Id, entity.SurveyId, entity.Metadata, entity.CreatedAt);
}
