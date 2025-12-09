using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Shared.Abstractions;
using WebApi.Surveys.Application.Commands.UpdateSurveyQuestionsCommand;
using WebApi.Surveys.Application.Dtos;

namespace WebApi.Surveys.Application.Endpoints;

public static class CompaniesEndpoints
{
    public static void MapSurveysEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/surveys");

        group
            .MapPatch("/{surveyId}/questions", UpdateSurveyQuestions)
            .WithName("UpdateSurveyQuestions")
            .WithOpenApi();
    }

    public static async Task<Results<Ok, BadRequest<Error>>> UpdateSurveyQuestions(
        [FromRoute] Guid surveyId,
        [FromBody] UpdateSurveyQuestionsRequest request,
        UpdateSurveyQuestionsCommandHandler handler
    )
    {
        var query = new UpdateSurveyQuestionsCommand(surveyId, request.Questions);

        var result = await handler.Handle(query);

        if (result.IsFailure)
        {
            return TypedResults.BadRequest(result.Error);
        }

        return TypedResults.Ok();
    }
}
