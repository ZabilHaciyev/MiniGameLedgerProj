using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public sealed record Entry
    {
        public Guid AccountId { get; private set; }
        public Money Amount { get; private set; } = default!;

        private Entry() { }   // только для EF

        public Entry(Guid accountId, Money amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}
