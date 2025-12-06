using WebApi.Surveys.Application.Dtos;

namespace WebApi.Companies.Application.Dtos;

public record CompanyDto(
    Guid Id,
    string Name,
    string Slug,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<SurveyDto> Surveys
);
