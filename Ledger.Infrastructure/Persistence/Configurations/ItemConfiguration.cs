using Ledger.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Infrastructure.Persistence.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> b)
        {
            b.ToTable("items");
            b.HasKey(i => i.Id);
            b.Property(i => i.Id).HasMaxLength(50).ValueGeneratedNever();
            b.Property(i => i.Name).HasMaxLength(100).IsRequired();
            b.Property(i => i.ImageUrl).HasMaxLength(300).IsRequired();

            b.Property(i => i.CategoryId).HasColumnName("category_id").HasMaxLength(50);
            b.HasOne<Category>().WithMany()
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasIndex(i => i.CategoryId);

            b.OwnsMany(i => i.Prices, ConfigurePrices);
            b.Navigation(i => i.Prices).UsePropertyAccessMode(PropertyAccessMode.Field);
        }

        private static void ConfigurePrices(OwnedNavigationBuilder<Item, ItemPrice> p)
        {
            p.ToTable("item_prices");
            p.WithOwner().HasForeignKey("item_id");
            p.Property<int>("id").ValueGeneratedOnAdd();
            p.HasKey("id");

            p.Property(x => x.Currency).HasColumnName("currency").HasMaxLength(3);
            p.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 4);

            p.HasIndex("item_id", "Currency").IsUnique();
        }
    }
}
