using Ledger.Domain;
using Ledger_Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Infrastructure.Persistence
{
    public sealed class ItemRepository(LedgerDbContext db) : IItemRepository
    {
        public async Task<Item?> GetAsync(string id, CancellationToken ct = default)
        {
            return await db.Items.FindAsync([id], ct);
        }

        public async Task<List<Item>> ListAsync(CancellationToken ct = default)
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(
                db.Items.OrderBy(i => i.Name), ct);
        }
    }
}
