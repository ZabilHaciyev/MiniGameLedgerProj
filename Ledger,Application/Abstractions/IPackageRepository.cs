using Ledger.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger_Application.Abstractions
{
    public interface IPackageRepository
    {
        Task<Package?> GetAsync(string id, CancellationToken ct = default);
        Task<List<Package>> ListAsync(CancellationToken ct = default);
    }
}
