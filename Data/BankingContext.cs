using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestSENG.Models;

namespace TestSENG.Data
{
    public class BankingContext : DbContext
    {
        public BankingContext (DbContextOptions<BankingContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Account { get; set; } = default!;

        public DbSet<Customer> Customer { get; set; } = default!;

        public DbSet<Deposit> Deposit { get; set; } = default!;

        public DbSet<TransferMoney> TransferMoney { get; set; } = default!;
        public DbSet<Withdraw> Withdraw { get; set; } = default!;
        public DbSet<Log_Account> Log_Account { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Deposit>().ToTable("Deposit");
            modelBuilder.Entity<TransferMoney>().ToTable("TransferMoney");
            modelBuilder.Entity<Withdraw>().ToTable("Withdraw");
            modelBuilder.Entity<Log_Account>().ToTable("Log_Account");

        }
    }
}

