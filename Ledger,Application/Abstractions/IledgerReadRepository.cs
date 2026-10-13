using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger_Application.Abstractions
{
    public interface ILedgerReadRepository
    {
        Task<decimal> GetBalanceAsync(Guid accountId, CancellationToken ct = default);
    }
}
