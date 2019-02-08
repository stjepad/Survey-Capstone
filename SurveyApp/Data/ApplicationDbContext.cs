//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;

//namespace SurveyApp.Data
//{
//    public class ApplicationDbContext : IdentityDbContext
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Text;
using SurveyApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SurveyApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerSurveyInstance> AnswersSurveyInstances { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyInstance> SurveyInstances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            modelBuilder.Entity<SurveyInstance>()
                .Property(si => si.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            //// Restrict deletion of related order when OrderProducts entry is removed
            modelBuilder.Entity<Answer>()
                .HasMany(asi => asi.AnswerSurveyInstances)
                .WithOne(a => a.Answer)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Product>()
            //    .Property(b => b.DateCreated)
            //    .HasDefaultValueSql("GETDATE()");

            //// Restrict deletion of related product when OrderProducts entry is removed
            modelBuilder.Entity<SurveyInstance>()
                .HasMany(asi => asi.AnswersSurveyInstances)
                .WithOne(si => si.SurveyInstance)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<PaymentType>()
            //    .Property(b => b.DateCreated)
            //    .HasDefaultValueSql("GETDATE()");

            ApplicationUser user = new ApplicationUser
            {
                FirstName = "admin",
                LastName = "admin",
                //CompanyName = "Tailgate Brewery",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);


            // Survey data 

            modelBuilder.Entity<Survey>().HasData(
                new Survey()
                {
                    SurveyId = 1,
                    UserId = user.Id,
                    Name = "Weekend FeedbacK",
                    Published = false,
                }
            );

            // SurveyInstance data

            modelBuilder.Entity<SurveyInstance>().HasData(
                new SurveyInstance()
                {
                    SurveyInstanceId = 1,
                    SurveyId = 1
                }
                );


            //Question data

            modelBuilder.Entity<Question>().HasData(
                new Question()
                {
                    QuestionId = 1,
                    Content = "How many people were in your party?",
                    SurveyId = 1
                },
                new Question()
                {
                QuestionId = 2,
                    Content = "How many beers did you drink?",
                    SurveyId = 1
                },
                new Question()
                {
                QuestionId = 3,
                    Content = "How much time did you spend here?",
                    SurveyId = 1
                },
                new Question()
                {
                QuestionId = 4,
                    Content = "Did you order food?",
                    SurveyId = 1
                },
                new Question()
                {
                    QuestionId = 5,
                    Content = "How would you rate your experience?",
                    SurveyId = 1
                }

            );
                
            // Answers data

            modelBuilder.Entity<Answer>().HasData(

                // "How many people were in your party?"
                new Answer()
                {
                    AnswerId = 1,
                    Content = "1",
                    QuestionId = 1
                },
                new Answer()
                {
                AnswerId = 2,
                    Content = "3",
                    QuestionId = 1
                },
                new Answer()
                {
                AnswerId = 3,
                    Content = "5",
                    QuestionId = 1
                },
                new Answer()
                {
                AnswerId = 4,
                    Content = "10",
                    QuestionId = 1
                },

                // "How many beers did you drink?"

                new Answer()
                {
                    AnswerId = 5,
                    Content = "1 - 2",
                    QuestionId = 2
                },
                new Answer()
                {
                    AnswerId = 6,
                    Content = "3 - 5",
                    QuestionId = 2
                },
                new Answer()
                {
                    AnswerId = 7,
                    Content = "More than 6",
                    QuestionId = 2
                },
                new Answer()
                {
                    AnswerId = 8,
                    Content = "I did not drink",
                    QuestionId = 2
                },

                // "How much time did you spend here?"

                new Answer()
                {
                    AnswerId = 9,
                    Content = " 30 minutes",
                    QuestionId = 3
                },
                new Answer()
                {
                    AnswerId = 10,
                    Content = " 1 hour",
                    QuestionId = 3
                },
                new Answer()
                {
                    AnswerId = 11,
                    Content = " 2 - 3 hours",
                    QuestionId = 3
                },
                new Answer()
                {
                    AnswerId = 12,
                    Content = "More than 3 hours",
                    QuestionId = 3
                },

                // "Did you order food?"

                new Answer()
                {
                    AnswerId = 13,
                    Content = "No",
                    QuestionId = 4
                },
                new Answer()
                {
                    AnswerId = 14,
                    Content = " Appetizers",
                    QuestionId = 4
                },
                new Answer()
                {
                    AnswerId = 15,
                    Content = "Main course",
                    QuestionId = 4
                },
                new Answer()
                {
                    AnswerId = 16,
                    Content = "Appetizer and main course",
                    QuestionId = 4
                },
                // "How would you rate your experience?"

                new Answer()
                {
                    AnswerId = 17,
                    Content = "Fantastic",
                    QuestionId = 5
                },
                new Answer()
                {
                    AnswerId = 18,
                    Content = " Good",
                    QuestionId = 5
                },
                new Answer()
                {
                    AnswerId = 19,
                    Content = "Poor",
                    QuestionId = 5
                },
                new Answer()
                {
                    AnswerId = 20,
                    Content = "I am not comming back",
                    QuestionId = 5
                }

            );

            modelBuilder.Entity<AnswerSurveyInstance>().HasData(
                new AnswerSurveyInstance()
                {   
                    AnswerSurveyInstanceId = 1,
                    SurveyInstanceId = 1,
                    AnswerId = 1
                }
                
            );
        }
    }
}
