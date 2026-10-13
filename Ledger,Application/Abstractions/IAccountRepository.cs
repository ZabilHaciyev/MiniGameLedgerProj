using Ledger.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger_Application.Abstractions
{

    public interface IAccountRepository
    {
        Task<Account?> GetAsync(Guid id, CancellationToken ct = default);
        Task<List<Account>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default);
        Task<bool> NameExistsAsync(string name, CancellationToken ct = default);
        Task AddAsync(Account account, CancellationToken ct = default);
    }
}
