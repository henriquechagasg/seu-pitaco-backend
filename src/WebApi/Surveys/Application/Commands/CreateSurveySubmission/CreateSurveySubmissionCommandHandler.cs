using Microsoft.EntityFrameworkCore;
using WebApi.Shared.Abstractions;
using WebApi.Shared.Infrastructure;
using WebApi.Surveys.Application.Dtos;
using WebApi.Surveys.Domain.Entities;

namespace WebApi.Surveys.Application.Commands.CreateSurveySubmission;

public class CreateSurveySubmissionCommandHandler(AppDbContext _context)
{
    public async Task<Result<SurveySubmissionDto>> Handle(CreateSurveySubmissionCommand command)
    {
        var survey = await _context
            .Surveys.Include(s => s.Questions)
            .FirstOrDefaultAsync(s => s.Id == command.SurveyId);

        if (survey is null)
        {
            return new Error("NotFoundError", "Pesquisa não encontrada.");
        }

        List<SurveyAnswer> answers = [];

        foreach (var a in command.Answers)
        {
            var question = survey.Questions.FirstOrDefault(q => q.Id == a.QuestionId);

            if (question is null)
                return new Error("NotFoundError", "Pergunta da pesquisa não encontrada.");

            var createAnswerInput = new CreateSurveyAnswerInput(
                question,
                a.Type,
                a.NumericValue,
                a.TextValue,
                a.ExtraComment
            );

            var answerResult = SurveyAnswer.Create(createAnswerInput);

            if (answerResult.IsFailure)
                return answerResult.Error;

            answers.Add(answerResult.Value);
        }

        var input = new CreateSurveySubmissionInput(command.SurveyId, answers);
        var submission = SurveySubmission.Create(input);

        if (submission.IsFailure)
        {
            return submission.Error;
        }

        _context.SurveySubmissions.Add(submission.Value);

        await _context.SaveChangesAsync();

        return submission.Value.ToDto();
    }
}
