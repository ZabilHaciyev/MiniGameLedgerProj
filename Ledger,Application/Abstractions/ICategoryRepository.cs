using Ledger.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger_Application.Abstractions
{
    public interface ICategoryRepository
    {
        Task<List<Category>> ListAsync(CancellationToken ct = default);
    }
}
