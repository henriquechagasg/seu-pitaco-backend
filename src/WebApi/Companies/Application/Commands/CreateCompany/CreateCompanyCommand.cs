using System.ComponentModel.DataAnnotations;

namespace WebApi.Companies.Application.Commands.CreateCompany;

public record CreateCompanyCommand
{
    [Required]
    [MaxLength(150)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Slug { get; set; }
}
