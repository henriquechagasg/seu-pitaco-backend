using Microsoft.EntityFrameworkCore;
using WebApi.Companies.Application.Dtos;
using WebApi.Companies.Application.Mapping;
using WebApi.Shared.Abstractions;
using WebApi.Shared.Infrastructure;
using WebApi.Surveys.Application.Dtos;

namespace WebApi.Companies.Application.Queries.ShowCompanySurvey;

public class ShowCompanySurveyQueryHandler(AppDbContext _context)
{
    public async Task<Result<CompanySurveyDto>> Handle(ShowCompanySurveyQuery query)
    {
        var company = await _context
            .Companies.Include(c => c.Surveys.Where(s => s.IsDefault))
                .ThenInclude(s => s.Questions)
            .FirstOrDefaultAsync(c => c.Slug == query.Slug);

        if (company == null)
        {
            return new Error("NotFoundError", "Empresa não encontrada.");
        }

        var survey = company.Surveys.FirstOrDefault(s => s.IsDefault);

        if (survey == null)
        {
            return new Error("NotFoundError", "Pesquisa padrão da empresa não encontrada.");
        }

        return new CompanySurveyDto(
            survey.Id,
            company.Name,
            survey.Title,
            [.. survey.Questions.Select(q => q.ToDto())],
            survey.CreatedAt,
            survey.UpdatedAt
        );
    }
}
