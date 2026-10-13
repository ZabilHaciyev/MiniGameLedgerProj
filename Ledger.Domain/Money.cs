using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public sealed record Money
    {
        public decimal Amount { get; private set; }
        public Currency Currency { get; private set; } = default!;

        private Money() { }   

        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public Money Negate() => new(-Amount, Currency);

        public static Money operator +(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new DomainException("Cannot add money in different currencies.");
            return new Money(a.Amount + b.Amount, a.Currency);
        }
    }
}
