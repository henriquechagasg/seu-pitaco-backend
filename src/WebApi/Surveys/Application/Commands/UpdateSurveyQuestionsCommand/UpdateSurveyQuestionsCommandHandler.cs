using Microsoft.EntityFrameworkCore;
using WebApi.Shared.Abstractions;
using WebApi.Shared.Infrastructure;
using WebApi.Surveys.Application.Dtos;
using WebApi.Surveys.Application.Mappings;

namespace WebApi.Surveys.Application.Commands.UpdateSurveyQuestionsCommand;

public class UpdateSurveyQuestionsCommandHandler(AppDbContext _context)
{
    public async Task<Result<SurveyDto>> Handle(UpdateSurveyQuestionsCommand command)
    {
        var survey = await _context
            .Surveys.Include(s => s.Questions)
            .FirstOrDefaultAsync(s => s.Id == command.SurveyId);

        if (survey == null)
        {
            return new Error("NotFoundError", "Pesquisa não encontrada.");
        }

        survey.UpdateQuestions([.. command.Questions.Select(q => q.ToEntity())]);

        await _context.SaveChangesAsync();

        return survey.ToDto();
    }
}
