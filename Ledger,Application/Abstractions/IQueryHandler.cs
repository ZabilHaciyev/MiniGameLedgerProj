using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger_Application.Abstractions
{
    public interface IQueryHandler<in TQuery, TResult>
    {
        Task<TResult> HandleAsync(TQuery query, CancellationToken ct = default);
    }
}
