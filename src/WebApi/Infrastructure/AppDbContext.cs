using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Company> Companies { get; set; }
    }
}
