using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger_Application.Transactions.PostTransaction
{
    public sealed record PostTransactionCommand(List<EntryDto> Entries, string? IdempotencyKey = null);
}
