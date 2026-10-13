using Ledger_Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Infrastructure.Persistence
{

    public sealed class LedgerReadRepository(LedgerDbContext db) : ILedgerReadRepository
    {
        public async Task<decimal> GetBalanceAsync(Guid accountId, CancellationToken ct = default)
        {
            var amounts = db.Transactions
                .SelectMany(t => t.Entries)
                .Where(e => e.AccountId == accountId)
                .Select(e => e.Amount.Amount);

            return await EntityFrameworkQueryableExtensions.SumAsync(amounts, ct);
        }
    }
}
