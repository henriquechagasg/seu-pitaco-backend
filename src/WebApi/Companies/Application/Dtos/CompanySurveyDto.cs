using WebApi.Surveys.Application.Dtos;

namespace WebApi.Companies.Application.Dtos;

public record CompanySurveyDto(
    Guid Id,
    string CompanyName,
    string? Title,
    List<SurveyQuestionDto> Questions,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
