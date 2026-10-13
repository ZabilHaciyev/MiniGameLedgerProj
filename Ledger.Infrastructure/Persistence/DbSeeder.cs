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

            await db.SaveChangesAsync();
        }
    }
}
