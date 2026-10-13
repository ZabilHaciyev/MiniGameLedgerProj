using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger_Application.Transactions.PostTransaction
{
    public sealed record EntryDto(Guid AccountId, decimal Amount, string Currency);

}
