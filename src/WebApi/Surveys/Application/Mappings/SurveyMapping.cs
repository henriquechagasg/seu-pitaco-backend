using System;
using WebApi.Surveys.Application.Dtos;
using WebApi.Surveys.Domain.Entities;

namespace WebApi.Surveys.Application.Mappings;

public static class SurveyMapping
{
    public static SurveyDto ToDto(this Survey survey) =>
        new(survey.Id, survey.Title, survey.IsDefault, survey.CreatedAt);
}
