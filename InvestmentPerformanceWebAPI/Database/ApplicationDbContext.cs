using InvestmentPerformanceWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;

namespace InvestmentPerformanceWebAPI.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }

        /// <summary>
        /// Overriding OnModelCreating to seed initial data into the database.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1, // primary key
                    Username = "testaccount",
                    Email = "testaccount@test.com",
                }
            );

            // Add transactions for user with UserId = 1
            modelBuilder.Entity<Transaction>().HasData(
                
                new Transaction
                {
                    Id = 1, // primary key
                    Type = TransactionType.Buy,
                    Name = "International Business Machines",
                    Symbol = "IBM",
                    Quantity = 10,
                    SharePriceAtPurchase = 282.16,
                    CurrentSharePrice = 283.50,
                    TransactionTime = DateTime.UtcNow.AddYears(-1),
                    UserId = 1 // foreign key to User
                },
                new Transaction
                {
                    Id = 2, // primary key
                    Type = TransactionType.Buy,
                    Name = "Fidelity Total Market Index Fund",
                    Symbol = "FSKAX",
                    Quantity = 150,
                    SharePriceAtPurchase = 184.29,
                    CurrentSharePrice = 200.10,
                    TransactionTime = DateTime.UtcNow,
                    UserId = 1 // foreign key to User
                },
                new Transaction
                {
                    Id = 3, // primary key
                    Type = TransactionType.Buy,
                    Name = "Microsoft",
                    Symbol = "MSFT",
                    SharePriceAtPurchase = 282.16,
                    CurrentSharePrice = 300.04,
                    Quantity = 1,
                    TransactionTime = DateTime.UtcNow.AddDays(-4),
                    UserId = 1 // foreign key to User
                },
                new Transaction
                {
                    Id = 4, // primary key
                    Type = TransactionType.Buy,
                    Name = "Vitreous Glass Inc",
                    Symbol = "VCI",
                    Quantity = 5,
                    SharePriceAtPurchase = 6.16,
                    CurrentSharePrice = 8.02,
                    TransactionTime = DateTime.UtcNow.AddYears(-2),
                    UserId = 1 // foreign key to User
                },
                 new Transaction
                 {
                     Id = 5, // primary key
                     Type = TransactionType.Buy,
                     Name = "Corsair",
                     Symbol = "CRSR",
                     Quantity = 2,
                     SharePriceAtPurchase = 8.72,
                     CurrentSharePrice = 8.02,
                     TransactionTime = DateTime.UtcNow.AddDays(-2),
                     UserId = 1 // foreign key to User
                 }
            );
        } 
    }
}
