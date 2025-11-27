using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Company> Companies { get; set; }
    }
}
