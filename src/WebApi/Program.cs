using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using WebApi.Companies.Application.Commands.CreateCompany;
using WebApi.Companies.Application.Endpoints;
using WebApi.Companies.Application.Queries.ListCompanies;
using WebApi.Companies.Application.Queries.ShowCompany;
using WebApi.Companies.Application.Queries.ShowCompanyReport;
using WebApi.Companies.Application.Queries.ShowCompanySurvey;
using WebApi.Shared.Infrastructure;
using WebApi.Surveys.Application.Commands.CreateSurveySubmission;
using WebApi.Surveys.Application.Commands.UpdateSurveyQuestions;
using WebApi.Surveys.Application.Endpoints;
using WebApi.Surveys.Domain.Repositories;
using WebApi.Surveys.Infra.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var dataSourceBuilder = new NpgsqlDataSourceBuilder(
        builder.Configuration.GetConnectionString("SeuPitacoDb")
    );

    dataSourceBuilder.EnableDynamicJson();

    var dataSource = dataSourceBuilder.Build();

    options.UseNpgsql(dataSource).UseSnakeCaseNamingConvention();
});

var awsOptions = builder.Configuration.GetAWSOptions();
builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();

builder.Services.AddScoped<ISurveysRepository, DynamoDbSurveysRepository>();
builder.Services.AddScoped<ShowCompanyReportQuery>();
builder.Services.AddScoped<CreateCompanyCommandHandler>();
builder.Services.AddScoped<ListCompaniesQueryHandler>();
builder.Services.AddScoped<ShowCompanyQueryHandler>();
builder.Services.AddScoped<UpdateSurveyQuestionsCommandHandler>();
builder.Services.AddScoped<CreateSurveySubmissionCommandHandler>();
builder.Services.AddScoped<ShowCompanySurveyQueryHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCompaniesEndpoints();
app.MapSurveysEndpoints();

app.Run();
