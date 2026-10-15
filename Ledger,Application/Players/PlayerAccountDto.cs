using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger_Application.Players
{
    public sealed record PlayerAccountDto(Guid Id, string Currency, decimal Balance);

}
