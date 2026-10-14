using Ledger.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger_Application.Abstractions
{
    public interface ITransactionRepository
    {
        Task AddAsync(LedgerTransaction transaction, CancellationToken ct = default);
        Task<Guid?> FindIdByKeyAsync(string key, CancellationToken ct = default);
    }
}
