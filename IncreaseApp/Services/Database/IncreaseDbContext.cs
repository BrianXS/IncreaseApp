using System.Collections.Generic;
using IncreaseApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace IncreaseApp.Services.Database
{
    public class IncreaseDbContext : DbContext
    {
        public IncreaseDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> Details { get; set; }
        public DbSet<TransactionDiscount> Discounts { get; set; }
    }
}