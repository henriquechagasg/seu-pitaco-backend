using System.ComponentModel.DataAnnotations;
using WebApi.Shared.Core.Enums;

namespace WebApi.Surveys.Domain.Entities
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Slug { get; set; } = "";

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Phone]
        public string PhoneNumber { get; set; } = "";

        public BusinessSegment Segment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = true;
    }
}
