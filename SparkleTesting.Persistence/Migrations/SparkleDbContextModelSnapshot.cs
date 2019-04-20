﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SparkleTesting.Persistence;

namespace SparkleTesting.Persistence.Migrations
{
    [DbContext(typeof(SparkleDbContext))]
    partial class SparkleDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AttemptId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("QuestionId");

                    b.Property<int>("QuestionPoints");

                    b.Property<string>("QuestionText");

                    b.HasKey("Id");

                    b.HasIndex("AttemptId", "QuestionId")
                        .IsUnique();

                    b.ToTable("Answers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Answer");
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.AnswerOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnswerId");

                    b.Property<bool>("IsChoosen");

                    b.Property<bool>("IsCorrect");

                    b.Property<int>("OptionId");

                    b.Property<string>("OptionText");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId", "OptionId")
                        .IsUnique();

                    b.ToTable("AnswerOptions");
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.Attempt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Assigned");

                    b.Property<TimeSpan?>("ElapsedTime");

                    b.Property<int?>("MarkId");

                    b.Property<TimeSpan?>("MaxTime");

                    b.Property<int?>("PointsAcquired");

                    b.Property<DateTime?>("StartTime");

                    b.Property<int>("TestId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("MarkId");

                    b.HasIndex("TestId");

                    b.HasIndex("UserId");

                    b.ToTable("Attempts");
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.FilledPass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnswerStrings");

                    b.Property<int?>("PassFillingAnswerId");

                    b.Property<int>("SortOrder");

                    b.Property<string>("UserAnswer");

                    b.HasKey("Id");

                    b.HasIndex("PassFillingAnswerId");

                    b.ToTable("FilledPass");
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsCorrect");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("QuestionId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.PassFilling", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnswerStrings");

                    b.Property<int?>("PassFillingQuestionId");

                    b.Property<int>("SortOrder");

                    b.HasKey("Id");

                    b.HasIndex("PassFillingQuestionId");

                    b.ToTable("PassFilling");
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Points");

                    b.Property<int>("SortOrder");

                    b.Property<int>("TestId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Questions");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Question");
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<TimeSpan>("AttemptTime");

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("InProgress");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Test");
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.TestMark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<int>("PointsThreshold");

                    b.Property<int>("TestId");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("TestMarks");
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Patronymic");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<byte[]>("Photo");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("StudyYear");

                    b.Property<string>("Surname");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.OptionsAnswer", b =>
                {
                    b.HasBaseType("SparkleTesting.Domain.Entities.Answer");

                    b.HasDiscriminator().HasValue("OptionsAnswer");
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.PassFillingAnswer", b =>
                {
                    b.HasBaseType("SparkleTesting.Domain.Entities.Answer");

                    b.HasDiscriminator().HasValue("PassFillingAnswer");
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.OptionsQuestion", b =>
                {
                    b.HasBaseType("SparkleTesting.Domain.Entities.Question");

                    b.Property<int>("Type");

                    b.HasDiscriminator().HasValue("OptionsQuestion");
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.PassFillingQuestion", b =>
                {
                    b.HasBaseType("SparkleTesting.Domain.Entities.Question");

                    b.HasDiscriminator().HasValue("PassFillingQuestion");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SparkleTesting.Domain.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SparkleTesting.Domain.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SparkleTesting.Domain.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SparkleTesting.Domain.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.Answer", b =>
                {
                    b.HasOne("SparkleTesting.Domain.Entities.Attempt", "Attempt")
                        .WithMany("Answers")
                        .HasForeignKey("AttemptId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.AnswerOption", b =>
                {
                    b.HasOne("SparkleTesting.Domain.Entities.OptionsAnswer", "Answer")
                        .WithMany("Options")
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.Attempt", b =>
                {
                    b.HasOne("SparkleTesting.Domain.Entities.TestMark", "Mark")
                        .WithMany()
                        .HasForeignKey("MarkId");

                    b.HasOne("SparkleTesting.Domain.Entities.Test", "Test")
                        .WithMany()
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SparkleTesting.Domain.Entities.User", "User")
                        .WithMany("Attempts")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.FilledPass", b =>
                {
                    b.HasOne("SparkleTesting.Domain.Entities.PassFillingAnswer")
                        .WithMany("FilledPasses")
                        .HasForeignKey("PassFillingAnswerId");
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.Option", b =>
                {
                    b.HasOne("SparkleTesting.Domain.Entities.OptionsQuestion", "Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.PassFilling", b =>
                {
                    b.HasOne("SparkleTesting.Domain.Entities.PassFillingQuestion")
                        .WithMany("Passes")
                        .HasForeignKey("PassFillingQuestionId");
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.Question", b =>
                {
                    b.HasOne("SparkleTesting.Domain.Entities.Test", "Test")
                        .WithMany("Questions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SparkleTesting.Domain.Entities.TestMark", b =>
                {
                    b.HasOne("SparkleTesting.Domain.Entities.Test", "Test")
                        .WithMany("Marks")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
