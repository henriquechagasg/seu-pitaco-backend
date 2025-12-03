using System;
using WebApi.Surveys.Domain.Entities;

namespace WebApi.Surveys.Domain.Repositories;

public interface ISurveysRepository
{
    Task<List<Survey>> FindMany();
}
