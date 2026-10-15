using Ledger.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> b)
        {
            b.ToTable("categories");
            b.HasKey(c => c.Id);
            b.Property(c => c.Id).HasMaxLength(50).ValueGeneratedNever();
            b.Property(c => c.Name).HasMaxLength(100).IsRequired();
        }
    }
}
