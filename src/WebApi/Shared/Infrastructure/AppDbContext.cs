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
        public DbSet<SurveySubmission> SurveySubmissions { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(150);

                entity.Property(e => e.Slug).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Slug).IsUnique();

                entity
                    .HasMany(c => c.Surveys)
                    .WithOne(s => s.Company)
                    .HasForeignKey(s => s.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity
                    .HasMany(co => co.Customers)
                    .WithOne(c => c.Company)
                    .HasForeignKey(c => c.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(150);
                entity.Property(e => e.Contact).HasMaxLength(100);
            });

            builder.Entity<Survey>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);

                entity
                    .HasMany(s => s.Submissions)
                    .WithOne(sub => sub.Survey)
                    .HasForeignKey(sub => sub.SurveyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity
                    .HasMany(s => s.Questions)
                    .WithOne(q => q.Survey)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<SurveyQuestion>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title).IsRequired().HasMaxLength(300);

                entity.Property(e => e.Options).HasColumnType("json");
            });

            builder.Entity<SurveySubmission>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.HasIndex(s => new { s.SurveyId, s.CreatedAt });

                entity.Property(s => s.CreatedAt).IsRequired();

                entity
                    .HasMany(s => s.Answers)
                    .WithOne(sa => sa.Submission)
                    .HasForeignKey(sa => sa.SubmissionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<SurveyAnswer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(a => new { a.SubmissionId, a.CreatedAt });
            });
        }
    }
}
