using Ledger.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Infrastructure.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> b)
        {
            b.ToTable("accounts");
            b.HasKey(a => a.Id);
            b.Property(a => a.Id).ValueGeneratedNever();
            b.Property(a => a.Name).HasMaxLength(100).IsRequired();

            b.Property(a => a.Currency)
                .HasColumnName("currency")
                .HasMaxLength(3)
                .HasConversion(c => c.Code, code => new Currency(code));

            b.HasIndex(a => a.Name).IsUnique();
        }
    }
}
