using Ledger.Domain;
using Ledger.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Infrastructure
{
    public class LedgerDbContext(DbContextOptions<LedgerDbContext> options) : DbContext(options)
    {
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<LedgerTransaction> Transactions => Set<LedgerTransaction>();
        public DbSet<Package> Packages => Set<Package>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Item> Items => Set<Item>();
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<Currency>()
                .HaveConversion<CurrencyConverter>()
                .HaveMaxLength(3);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(typeof(LedgerDbContext).Assembly);
    }
}
