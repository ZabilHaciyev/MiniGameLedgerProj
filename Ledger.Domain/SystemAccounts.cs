using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public static class SystemAccounts
    {

        public static readonly Guid GameBankGold = Guid.Parse("00000000-0000-0000-0000-000000000001");
        public static readonly Guid GameBankUsd = Guid.Parse("00000000-0000-0000-0000-000000000002");
        public static readonly Guid GameBankDiamonds = Guid.Parse("00000000-0000-0000-0000-000000000003");
        public static readonly Guid GameBankCoins = Guid.Parse("00000000-0000-0000-0000-000000000004");
        public static Guid BankFor(Currency currency)
        {
            switch (currency.Code)
            {
                case "USD":
                    return GameBankUsd;
                case "GLD":
                    return GameBankGold;
                case "DIA":
                    return GameBankDiamonds;
                case "COI":
                    return GameBankCoins;
                default:
                    throw new DomainException($"No bank account for currency {currency}.");
            }
        }

    }
}
