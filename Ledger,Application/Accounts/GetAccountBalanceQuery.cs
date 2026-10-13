using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger_Application.Accounts
{
    public sealed record GetAccountBalanceQuery(Guid AccountId);

}
