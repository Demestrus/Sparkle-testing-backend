using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SparkleTesting.Domain.Entities;
using SparkleTesting.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SparkleTesting.Persistence
{
    public class SparkleDbContext : IdentityDbContext<User>
    {
        public DbSet<Test> Test { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<TestMark> TestMarks { get; set; }
        public DbSet<Attempt> Attempts { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }

        public SparkleDbContext(DbContextOptions<SparkleDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new Exception("В SparkleDbContext отсутствует строка подключения.");
            }
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OptionsQuestion>();

            modelBuilder.Entity<PassFillingQuestion>();

            modelBuilder.Entity<OptionsAnswer>();

            modelBuilder.Entity<PassFillingAnswer>();

            modelBuilder.Entity<PassFilling>()
                .Property<string>("AnswerStrings")
                .HasField("_answerStrings");

            modelBuilder.Entity<PassFilling>()
                .Ignore(s => s.CorrectAnswers);

            modelBuilder.Entity<FilledPass>()
                .Property<string>("AnswerStrings")
                .HasField("_answerStrings");

            modelBuilder.Entity<FilledPass>()
               .Ignore(s => s.CorrectAnswers);

            modelBuilder.Entity<Answer>()
                .HasIndex(s => new { s.AttemptId, s.QuestionId })
                .IsUnique();

            modelBuilder.Entity<AnswerOption>()
                .HasIndex(s => new { s.AnswerId, s.OptionId })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        private void PreProcessing()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().Where(s=>s.State == EntityState.Added))
            {
                entry.Entity.CreateDate = DateTime.UtcNow;
            }

            foreach (var entry in ChangeTracker.Entries<ISoftDelete>().Where(s => s.State == EntityState.Deleted))
            {
                entry.Entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }

        public override int SaveChanges()
        {
            PreProcessing();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            PreProcessing();
            return await base.SaveChangesAsync(token);
        }
    }
}
