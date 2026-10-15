using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public sealed class Package
    {
        public string Id { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public decimal PriceUsd { get; private set; }
        public Currency RewardCurrency { get; private set; } = default!;
        public decimal RewardAmount { get; private set; }

        private Package() { }  

        public Package(string id, string name, decimal priceUsd, Currency rewardCurrency, decimal rewardAmount)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new DomainException("Package id is required.");
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Package name is required.");
            if (priceUsd <= 0)
                throw new DomainException("Package price must be positive.");
            if (rewardAmount <= 0)
                throw new DomainException("Package reward must be positive.");

            Id = id;
            Name = name;
            PriceUsd = priceUsd;
            RewardCurrency = rewardCurrency;
            RewardAmount = rewardAmount;
        }
    }

}
