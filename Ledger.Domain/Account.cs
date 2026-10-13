using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public sealed class Account
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = default!;
        public Currency Currency { get; private set; } = default!;

        private Account() { }   // только для EF

        public Account(Guid id, string name, Currency currency)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Account name is required.");

            Id = id;
            Name = name;
            Currency = currency;
        }
    }
}
