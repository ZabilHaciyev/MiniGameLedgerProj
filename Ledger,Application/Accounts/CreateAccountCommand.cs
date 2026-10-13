using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger_Application.Accounts
{
    public sealed record CreateAccountCommand(string Name, string Currency);
}
