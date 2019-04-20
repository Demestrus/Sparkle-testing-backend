using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SparkleTesting.Domain.Entities;
using System;

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

            modelBuilder.Entity<ShortAnswerQuestion>()
                .Property<string>("AnswerStrings")
                .HasField("_answerStrings");

            modelBuilder.Entity<ShortAnswerQuestion>()
                .Ignore(s => s.CorrectAnswers);

            modelBuilder.Entity<ShortAnswer>()
                .Property<string>("AnswerStrings")
                .HasField("_answerStrings");

            modelBuilder.Entity<ShortAnswer>()
               .Ignore(s => s.CorrectAnswers);

            modelBuilder.Entity<Answer>()
                .HasIndex(s => new { s.AttemptId, s.QuestionId })
                .IsUnique();

            modelBuilder.Entity<AnswerOption>()
                .HasIndex(s => new { s.AnswerId, s.OptionId })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
