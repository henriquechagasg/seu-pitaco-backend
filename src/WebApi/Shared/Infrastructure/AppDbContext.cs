using Microsoft.EntityFrameworkCore;
using WebApi.Companies.Domain.Entities;
using WebApi.Customers.Domain.Entities;
using WebApi.Surveys.Domain.Entities;

namespace WebApi.Shared.Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(150);

                entity.Property(e => e.Slug).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Slug).IsUnique();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(150);
                entity.Property(e => e.Contact).HasMaxLength(100);

                entity
                    .HasOne(c => c.Company)
                    .WithMany(co => co.Customers)
                    .HasForeignKey(c => c.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);

                entity
                    .HasOne(s => s.Company)
                    .WithMany(c => c.Surveys)
                    .HasForeignKey(s => s.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity
                    .HasMany(q => q.Questions)
                    .WithOne(s => s.Survey)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SurveyQuestion>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title).IsRequired().HasMaxLength(300);

                entity.Property(e => e.Metadata).HasColumnType("jsonb");

                entity
                    .HasOne(q => q.Survey)
                    .WithMany(s => s.Questions)
                    .HasForeignKey(q => q.SurveyId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SurveyAnswer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(a => new { a.SurveyId, a.CreatedAt });

                entity
                    .HasOne(a => a.Survey)
                    .WithMany()
                    .HasForeignKey(a => a.SurveyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity
                    .HasOne(a => a.Question)
                    .WithMany()
                    .HasForeignKey(a => a.QuestionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
