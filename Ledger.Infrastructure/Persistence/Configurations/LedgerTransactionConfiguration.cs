using Ledger.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.Infrastructure.Persistence.Configurations;

public class LedgerTransactionConfiguration : IEntityTypeConfiguration<LedgerTransaction>
{
    public void Configure(EntityTypeBuilder<LedgerTransaction> b)
    {
        b.ToTable("transactions");
        b.HasKey(t => t.Id);
        b.Property(t => t.Id).ValueGeneratedNever();
        b.Property(t => t.CreatedAt).HasColumnName("created_at");

        b.OwnsMany(t => t.Entries, ConfigureEntries);
    }

    private static void ConfigureEntries(OwnedNavigationBuilder<LedgerTransaction, Entry> e)
    {
        e.ToTable("entries");
        e.WithOwner().HasForeignKey("transaction_id");
        e.Property<int>("id").ValueGeneratedOnAdd();
        e.HasKey("id");

        e.Property(x => x.AccountId).HasColumnName("account_id");
        e.HasIndex(x => x.AccountId);

        e.OwnsOne(x => x.Amount, m => {
            m.Property(p => p.Amount).HasColumnName("amount").HasPrecision(18, 4);
            m.Property(p => p.Currency)
                .HasColumnName("currency")
                .HasMaxLength(3)
                .HasConversion(c => c.Code, code => new Currency(code));
        });
    }
}