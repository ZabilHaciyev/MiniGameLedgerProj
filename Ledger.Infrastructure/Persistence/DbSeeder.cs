using Ledger.Domain;
using Microsoft.EntityFrameworkCore;
namespace Ledger.Infrastructure.Persistence
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(LedgerDbContext db)
        {
            Account[] systemAccounts =
       [
 new(SystemAccounts.GameBankGold,     "GameBankGold",     new Currency("GLD")),
    new(SystemAccounts.GameBankUsd,      "GameBankUsd",      new Currency("USD")),
    new(SystemAccounts.GameBankDiamonds, "GameBankDiamonds", new Currency("DIA")),
    new(SystemAccounts.GameBankCoins,    "GameBankCoins",    new Currency("COI"))
       ];

            foreach (var account in systemAccounts)
            {
                if (!await db.Accounts.AnyAsync(a => a.Id == account.Id))
                    db.Accounts.Add(account);
            }
            foreach (var p in SystemPackages.All)
            {
                if (await db.Packages.FindAsync(p.Id) is null)
                    db.Packages.Add(new Package(p.Id, p.Name, p.PriceUsd, p.RewardCurrency, p.RewardAmount));
            }
            foreach (var c in SystemCategories.All)
            {
                if (await db.Categories.FindAsync(c.Id) is null)
                    db.Categories.Add(c);
            }

            foreach (var i in SystemItems.All)
            {
                if (await db.Items.FindAsync(i.Id) is null)
                {
                    var prices = i.Prices.Select(p => new ItemPrice(p.Currency, p.Amount));
                    db.Items.Add(new Item(i.Id, i.Name, i.CategoryId, i.ImageUrl, prices));
                }
            }
            await db.SaveChangesAsync();
        }
    }
}
