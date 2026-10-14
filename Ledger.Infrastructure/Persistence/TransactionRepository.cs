using Ledger.Domain;
using Ledger_Application.Abstractions;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Guid?> FindIdByKeyAsync(string key, CancellationToken ct = default)
        {
            return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(
                db.Transactions.Where(t => t.IdempotencyKey == key).Select(t => (Guid?)t.Id), ct);
        }

    }
}
