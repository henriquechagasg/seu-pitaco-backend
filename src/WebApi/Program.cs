using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.EntityFrameworkCore;
using WebApi.Companies.Application.Endpoints;
using WebApi.Companies.Application.Queries;
using WebApi.Shared.Infrastructure;
using WebApi.Surveys.Domain.Repositories;
using WebApi.Surveys.Infra.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

var awsOptions = builder.Configuration.GetAWSOptions();
builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();

builder.Services.AddScoped<ISurveysRepository, DynamoDbSurveysRepository>();
builder.Services.AddScoped<CompanyReportQuery>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCompaniesEndpoints();

app.Run();
