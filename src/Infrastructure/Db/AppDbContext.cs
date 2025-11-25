namespace Infrastructure.Db
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : DbContext(options),
            DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
