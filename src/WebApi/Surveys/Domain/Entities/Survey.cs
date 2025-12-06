using WebApi.Companies.Domain.Entities;

namespace WebApi.Surveys.Domain.Entities;

public class Survey
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string? Metadata { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Company Company { get; set; } = null!;
    public ICollection<SurveyQuestion> Questions { get; set; } = [];
}
