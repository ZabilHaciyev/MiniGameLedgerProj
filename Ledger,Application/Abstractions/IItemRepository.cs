using Ledger.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger_Application.Abstractions
{
    public interface IItemRepository
    {
        Task<Item?> GetAsync(string id, CancellationToken ct = default);
        Task<List<Item>> ListAsync(CancellationToken ct = default);
    }
}
