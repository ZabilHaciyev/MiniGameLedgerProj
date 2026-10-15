using Ledger.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Infrastructure.Persistence.Configurations
{
    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> b)
        {
            b.ToTable("packages");
            b.HasKey(p => p.Id);
            b.Property(p => p.Id).HasMaxLength(50).ValueGeneratedNever();
            b.Property(p => p.Name).HasMaxLength(100).IsRequired();
            b.Property(p => p.PriceUsd).HasPrecision(18, 4);
            b.Property(p => p.RewardAmount).HasPrecision(18, 4);
            b.Property(p => p.RewardCurrency).HasColumnName("reward_currency");
        }
    }
}
