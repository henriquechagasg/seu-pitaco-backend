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

        var surveyAnswersCount = _context.SurveyAnswers.Count(sa => sa.SurveyId == survey.Id);
        if (surveyAnswersCount > 0)
        {
            return new Error(
                "ForbiddenAction",
                "Não é possível atualizar uma pesquisa que contém respostas."
            );
        }

        survey.UpdateQuestions([.. command.Questions.Select(q => q.ToEntity())]);

        await _context.SaveChangesAsync();

        return survey.ToDto();
    }
}
