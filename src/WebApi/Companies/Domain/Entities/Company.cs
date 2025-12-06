using WebApi.Customers.Domain.Entities;
using WebApi.Surveys.Domain.Entities;

namespace WebApi.Companies.Domain.Entities
{
    public class Company
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public ICollection<Survey> Surveys { get; set; } = [];
        public ICollection<Customer> Customers { get; set; } = [];
    }
}
