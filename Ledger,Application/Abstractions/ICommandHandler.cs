using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger_Application.Abstractions
{
    public interface ICommandHandler<in TCommand, TResult>
    {
        Task<TResult> HandleAsync(TCommand command, CancellationToken ct = default);

    }
}
