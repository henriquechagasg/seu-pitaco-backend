using System;

namespace WebApi.Companies.Application.Dtos;

public class CompanyReportResultDto
{
    public int Count { get; set; }
    public required NpsSummary NpsSummary { get; set; }
}

public class NpsSummary()
{
    public float Score { get; set; }
    public int PromotersCount { get; set; }
    public int NeutralsCount { get; set; }
    public int DetractorsCount { get; set; }
    public required Dictionary<string, int> RatingDistribution { get; set; }
}
