using Microsoft.EntityFrameworkCore;
using WebApi.Surveys.Domain.Entities;

namespace WebApi.Shared.Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Company> Companies { get; set; }
    }
}
