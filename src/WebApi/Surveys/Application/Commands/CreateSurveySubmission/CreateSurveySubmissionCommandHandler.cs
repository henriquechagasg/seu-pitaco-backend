using Microsoft.EntityFrameworkCore;
using WebApi.Shared.Abstractions;
using WebApi.Shared.Core.Enums;
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

        foreach (var answerDto in command.Answers)
        {
            var answerResult = CreateAnswer(survey, answerDto);

            if (answerResult.IsFailure)
            {
                return answerResult.Error;
            }

            answers.Add(answerResult.Value);
        }

        var submission = SurveySubmission.Create(
            new CreateSurveySubmissionInput(command.SurveyId, answers)
        );

        if (submission.IsFailure)
        {
            return submission.Error;
        }

        _context.SurveySubmissions.Add(submission.Value);
        await _context.SaveChangesAsync();

        return submission.Value.ToDto();
    }

    private static Result<SurveyAnswer> CreateAnswer(Survey survey, CreateSurveyAnswerDto dto)
    {
        var question = survey.Questions.FirstOrDefault(q => q.Id == dto.QuestionId);

        if (question is null)
            return new Error("NotFoundError", "Pergunta da pesquisa não encontrada.");

        if (!Enum.IsDefined(typeof(QuestionType), dto.Type))
            return new Error("ValidationError", "Tipo de pergunta inválido.");

        return SurveyAnswer.Create(
            new CreateSurveyAnswerInput(
                question,
                dto.Type,
                dto.NumericValue,
                dto.TextValue,
                dto.ExtraComment
            )
        );
    }
}
