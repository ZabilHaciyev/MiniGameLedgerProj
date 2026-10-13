using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger_Application.Accounts
{
    public sealed record AccountBalanceDto(Guid AccountId, string Name, string Currency, decimal Balance);
}
