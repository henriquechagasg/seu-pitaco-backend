using Microsoft.EntityFrameworkCore;
using WebApi.Shared.Abstractions;
using WebApi.Shared.Infrastructure;
using WebApi.Surveys.Application.Dtos;

namespace WebApi.Surveys.Application.Commands.UpdateSurveyQuestions;

public class UpdateSurveyQuestionsCommandHandler(AppDbContext _context)
{
    public async Task<Result<SurveyDto>> Handle(UpdateSurveyQuestionsCommand command)
    {
        var survey = await _context
            .Surveys.Include(s => s.Questions)
            .Include(s => s.Submissions)
            .FirstOrDefaultAsync(s => s.Id == command.SurveyId);

        if (survey == null)
        {
            return new Error("NotFoundError", "Pesquisa não encontrada.");
        }

        var result = survey.UpdateQuestions([.. command.Questions.Select(q => q.ToEntity())]);

        if (result.IsFailure)
        {
            return result.Error;
        }

        await _context.SaveChangesAsync();

        return survey.ToDto();
    }
}
