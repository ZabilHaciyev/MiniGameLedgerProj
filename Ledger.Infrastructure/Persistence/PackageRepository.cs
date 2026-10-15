using Ledger.Domain;
using Microsoft.EntityFrameworkCore;
using Ledger_Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Infrastructure.Persistence
{
    public sealed class PackageRepository(LedgerDbContext db) :  IPackageRepository
    {
        public async Task<Package?> GetAsync(string id, CancellationToken ct = default)
        {
            return await db.Packages.FindAsync([id], ct);
        }

        public async Task<List<Package>> ListAsync(CancellationToken ct = default)
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(
                db.Packages.OrderBy(p => p.PriceUsd), ct);
        }
    }
}
