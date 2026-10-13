using Ledger.Domain;
using Ledger_Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Infrastructure.Persistence
{
    public sealed class TransactionRepository(LedgerDbContext db) : ITransactionRepository
    {
        public async Task AddAsync(LedgerTransaction transaction, CancellationToken ct = default)
        {
            db.Transactions.Add(transaction);
            await db.SaveChangesAsync(ct);
        }

    }
}
