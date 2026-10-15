using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{

    public static class SystemPackages
    {
        public static readonly Package GoldStandard =
            new("gold_standard", "Gold Standard", 10m, new Currency("GLD"), 100m);

        public static readonly Package GoldPro =
            new("gold_pro", "Gold Pro", 50m, new Currency("GLD"), 1000m);

        public static readonly Package DiamondStandard =
            new("diamond_standard", "Diamond Standard", 10m, new Currency("DIA"), 50m);

        public static readonly Package DiamondPro =
            new("diamond_pro", "Diamond Pro", 50m, new Currency("DIA"), 300m);

        public static readonly Package CoinStandard =
            new("coin_standard", "Coin Standard", 5m, new Currency("COI"), 500m);

        public static readonly Package CoinPro =
            new("coin_pro", "Coin Pro", 25m, new Currency("COI"), 3000m);

        public static readonly IReadOnlyList<Package> All =
        [
            GoldStandard, GoldPro,
        DiamondStandard, DiamondPro,
        CoinStandard, CoinPro
        ];

        public static Package Get(string id) =>
            All.FirstOrDefault(p => p.Id == id)
            ?? throw new DomainException("Unknown package.");
    }
}
