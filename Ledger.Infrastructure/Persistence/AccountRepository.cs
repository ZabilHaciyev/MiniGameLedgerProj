using Ledger.Domain;
using Ledger_Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Infrastructure.Persistence
{
    public sealed class AccountRepository(LedgerDbContext db) : IAccountRepository
    {
        public async Task<Account?> GetAsync(Guid id, CancellationToken ct = default)
        {
            return await db.Accounts.FindAsync([id], ct);
        }
        public async Task<bool> NameExistsAsync(string name, CancellationToken ct = default)
        {
            return await EntityFrameworkQueryableExtensions.AnyAsync(
                db.Accounts, 
                a => a.Name == name, 
                ct);
        }
        public async Task AddAsync(Account account, CancellationToken ct = default)
        {
            db.Accounts.Add(account);
            await db.SaveChangesAsync(ct);
        }
        public async Task<List<Account>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default)
        {
            var list = ids.ToList();
            return await EntityFrameworkQueryableExtensions.ToListAsync(
                db.Accounts.Where(a => list.Contains(a.Id)), ct);
        }
        public async Task<Account?> GetByNameAsync(string name, CancellationToken ct = default)
        {
            return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(
                db.Accounts, a => a.Name == name, ct);
        }
    }
}
