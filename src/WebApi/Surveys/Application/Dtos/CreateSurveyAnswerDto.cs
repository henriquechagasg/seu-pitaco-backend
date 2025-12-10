using System;
using WebApi.Shared.Core.Enums;
using WebApi.Surveys.Domain.Entities;

namespace WebApi.Surveys.Application.Dtos;

public record CreateSurveyAnswerDto(
    Guid QuestionId,
    QuestionType Type,
    int? NumericValue,
    string? TextValue,
    string? ExtraComment
);

public static class CreateSurveyAnswerMapping
{
    public static SurveyAnswer ToEntity(this CreateSurveyAnswerDto dto) =>
        new()
        {
            QuestionId = dto.QuestionId,
            Type = dto.Type,
            NumericValue = dto.NumericValue,
            TextValue = dto.TextValue,
            ExtraComment = dto.ExtraComment,
        };
}
