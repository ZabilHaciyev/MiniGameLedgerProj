using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public sealed record Currency
    {
        public string Code { get; private set; } = default!;

        public Currency(string code)
        {
            if (string.IsNullOrWhiteSpace(code) || code.Length != 3)
                throw new DomainException("Currency code must be 3 letters.");
            Code = code.ToUpperInvariant();
        }

        public override string ToString() => Code;
    }
}
