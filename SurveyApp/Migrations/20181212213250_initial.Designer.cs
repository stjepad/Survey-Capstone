﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SurveyApp.Data;

namespace SurveyApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20181212213250_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
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
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

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

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SurveyApp.Models.Answer", b =>
                {
                    b.Property<int>("AnswerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<int>("QuestionId");

                    b.HasKey("AnswerId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");

                    b.HasData(
                        new { AnswerId = 1, Content = "1", QuestionId = 1 },
                        new { AnswerId = 2, Content = "3", QuestionId = 1 },
                        new { AnswerId = 3, Content = "5", QuestionId = 1 },
                        new { AnswerId = 4, Content = "10", QuestionId = 1 },
                        new { AnswerId = 5, Content = "1 - 2", QuestionId = 2 },
                        new { AnswerId = 6, Content = "3 - 5", QuestionId = 2 },
                        new { AnswerId = 7, Content = "More than 6", QuestionId = 2 },
                        new { AnswerId = 8, Content = "I did not drink", QuestionId = 2 },
                        new { AnswerId = 9, Content = " 30 minutes", QuestionId = 3 },
                        new { AnswerId = 10, Content = " 1 hour", QuestionId = 3 },
                        new { AnswerId = 11, Content = " 2 - 3 hours", QuestionId = 3 },
                        new { AnswerId = 12, Content = "More than 3 hours", QuestionId = 3 },
                        new { AnswerId = 13, Content = "No", QuestionId = 4 },
                        new { AnswerId = 14, Content = " Appetizers", QuestionId = 4 },
                        new { AnswerId = 15, Content = "Main course", QuestionId = 4 },
                        new { AnswerId = 16, Content = "Appetizer and main course", QuestionId = 4 },
                        new { AnswerId = 17, Content = "Fantastic", QuestionId = 5 },
                        new { AnswerId = 18, Content = " Good", QuestionId = 5 },
                        new { AnswerId = 19, Content = "Poor", QuestionId = 5 },
                        new { AnswerId = 20, Content = "I am not comming back", QuestionId = 5 }
                    );
                });

            modelBuilder.Entity("SurveyApp.Models.AnswerSurveyInstance", b =>
                {
                    b.Property<int>("AnswerSurveyInstanceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnswerId");

                    b.Property<int>("SurveyInstanceId");

                    b.HasKey("AnswerSurveyInstanceId");

                    b.HasIndex("AnswerId");

                    b.HasIndex("SurveyInstanceId");

                    b.ToTable("AnswersSurveyInstances");

                    b.HasData(
                        new { AnswerSurveyInstanceId = 1, AnswerId = 1, SurveyInstanceId = 1 }
                    );
                });

            modelBuilder.Entity("SurveyApp.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("CompanyName")
                        .IsRequired();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

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

                    b.HasData(
                        new { Id = "a7fc0ad4-a3a7-40dd-b97f-79b2a9ed0ccf", AccessFailedCount = 0, CompanyName = "Tailgate Brewery", ConcurrencyStamp = "c43aa331-71f7-4d3f-a3db-012d81cb55a7", Email = "admin@admin.com", EmailConfirmed = true, FirstName = "admin", LastName = "admin", LockoutEnabled = false, NormalizedEmail = "ADMIN@ADMIN.COM", NormalizedUserName = "ADMIN@ADMIN.COM", PasswordHash = "AQAAAAEAACcQAAAAEOhCLm7cgKKQ8bXsEvLYuow4z8Agimrnp5dQq+YtywQWDau3fBZtUxHPbhh0mV12kw==", PhoneNumberConfirmed = false, SecurityStamp = "03d3b1a8-1749-4d40-b255-1bf9c45c7bac", TwoFactorEnabled = false, UserName = "admin@admin.com" }
                    );
                });

            modelBuilder.Entity("SurveyApp.Models.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<int>("SurveyId");

                    b.HasKey("QuestionId");

                    b.HasIndex("SurveyId");

                    b.ToTable("Questions");

                    b.HasData(
                        new { QuestionId = 1, Content = "How many people were in your party?", SurveyId = 1 },
                        new { QuestionId = 2, Content = "How many beers did you drink?", SurveyId = 1 },
                        new { QuestionId = 3, Content = "How much time did you spend here?", SurveyId = 1 },
                        new { QuestionId = 4, Content = "Did you order food?", SurveyId = 1 },
                        new { QuestionId = 5, Content = "How would you rate your experience?", SurveyId = 1 }
                    );
                });

            modelBuilder.Entity("SurveyApp.Models.Survey", b =>
                {
                    b.Property<int>("SurveyId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<bool>("Published");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("SurveyId");

                    b.HasIndex("UserId");

                    b.ToTable("Surveys");

                    b.HasData(
                        new { SurveyId = 1, Name = "Weekend FeedbacK", Published = false, UserId = "a7fc0ad4-a3a7-40dd-b97f-79b2a9ed0ccf" }
                    );
                });

            modelBuilder.Entity("SurveyApp.Models.SurveyInstance", b =>
                {
                    b.Property<int>("SurveyInstanceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("SurveyId");

                    b.HasKey("SurveyInstanceId");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyInstances");

                    b.HasData(
                        new { SurveyInstanceId = 1, DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), SurveyId = 1 }
                    );
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
                    b.HasOne("SurveyApp.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SurveyApp.Models.ApplicationUser")
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

                    b.HasOne("SurveyApp.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SurveyApp.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SurveyApp.Models.Answer", b =>
                {
                    b.HasOne("SurveyApp.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SurveyApp.Models.AnswerSurveyInstance", b =>
                {
                    b.HasOne("SurveyApp.Models.Answer", "Answer")
                        .WithMany("AnswerSurveyInstances")
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SurveyApp.Models.SurveyInstance", "SurveyInstance")
                        .WithMany("AnswersSurveyInstances")
                        .HasForeignKey("SurveyInstanceId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SurveyApp.Models.Question", b =>
                {
                    b.HasOne("SurveyApp.Models.Survey", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SurveyApp.Models.Survey", b =>
                {
                    b.HasOne("SurveyApp.Models.ApplicationUser", "User")
                        .WithMany("Surveys")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SurveyApp.Models.SurveyInstance", b =>
                {
                    b.HasOne("SurveyApp.Models.Survey", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
