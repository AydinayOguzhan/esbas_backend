using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Entities.Concrete;

namespace DataAccess.Concrete.Contexts
{
    public class EfContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }


        public DbSet<Student> Students { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>().HasData(
                new Gender { Id=1, Name = "Kadın", Status = true },
                new Gender { Id = 2, Name = "Erkek", Status = true }
            );

            modelBuilder.Entity<MaritalStatus>().HasData(
                new MaritalStatus { Id = 1, Name = "Evli", Status = true },
                new MaritalStatus { Id = 2, Name = "Bekar", Status = true }
            );

            modelBuilder.Entity<OperationClaim>().HasData(
                new OperationClaim { Id = 1, Name = "Admin", Status = true },
                new OperationClaim { Id = 2, Name = "User", Status = true }
            );
        }
    }
}
