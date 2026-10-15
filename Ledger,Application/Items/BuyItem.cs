using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger_Application.Items
{

    public sealed record BuyItemCommand(Guid PlayerAccountId, string ItemId,string Currency,string? IdempotencyKey = null);
}
