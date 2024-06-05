using EmployeeManagement.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EmployeeManagement.Core.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRoles>().HasData(
            new UserRoles { RoleID = 1, RoleName = "Admin" },
            new UserRoles { RoleID = 2, RoleName = "User" }
            );

            // Define seed data for Users
            modelBuilder.Entity<Users>().HasData(
                new Users
                {
                    ID = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    UserName = "admn",
                    Role = 1,
                    Password = "AFAztvXfuTIVjWXvLLQYd33y7PUZTT/S393EKDXAj8IFd0f5R9P0iZl0mLrvZh6fEA==" ,//admin
                },
                new Users
                {
                    ID = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    UserName = "jane.smith",
                    Role = 2,
                    Password = "AFAztvXfuTIVjWXvLLQYd33y7PUZTT/S393EKDXAj8IFd0f5R9P0iZl0mLrvZh6fEA==" //admin
                }
                );
        }
    }
}
