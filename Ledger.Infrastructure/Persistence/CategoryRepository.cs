using Ledger.Domain;
using Ledger_Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Infrastructure.Persistence
{
    public sealed class CategoryRepository(LedgerDbContext db) : ICategoryRepository
    {
        public async Task<List<Category>> ListAsync(CancellationToken ct = default)
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(
                db.Categories.OrderBy(c => c.Name), ct);
        }
    }
}
