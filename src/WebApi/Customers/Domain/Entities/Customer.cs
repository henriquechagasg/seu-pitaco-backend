using System;
using WebApi.Companies.Domain.Entities;

namespace WebApi.Customers.Domain.Entities;

public class Customer
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public required string Name { get; set; }
    public required string Cpf { get; set; }
    public string? Contact { get; set; }
    public int TotalFeedbacks { get; set; }
    public int FidelityPoints { get; set; }
    public DateTime? LastFeedbackDate { get; set; }
    public Company Company { get; set; } = null!;
}
