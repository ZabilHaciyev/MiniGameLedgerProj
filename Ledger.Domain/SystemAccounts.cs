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
    
}
}
