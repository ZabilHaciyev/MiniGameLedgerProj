using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public sealed record ItemPrice
    {
        public Currency Currency { get; private set; } = default!;
        public decimal Amount { get; private set; }

        private ItemPrice() { }   

        public ItemPrice(Currency currency, decimal amount)
        {
            if (amount <= 0)
                throw new DomainException("Price must be positive.");

            Currency = currency;
            Amount = amount;
        }
    }
}
