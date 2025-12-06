namespace WebApi.Surveys.Application.Dtos;

public record SurveyDto(Guid Id, string? Title, bool IsDefault, DateTime CreatedAt);
