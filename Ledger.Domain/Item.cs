using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Ledger.Domain
{
    public sealed class Item
    {
        private readonly List<ItemPrice> _prices = [];

        public string Id { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public string CategoryId { get; private set; } = default!;
        public string ImageUrl { get; private set; } = default!;
        public IReadOnlyList<ItemPrice> Prices => _prices;

        private Item() { } 

        public Item(string id, string name, string categoryId, string imageUrl, IEnumerable<ItemPrice> prices)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new DomainException("Item id is required.");
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Item name is required.");
            if (string.IsNullOrWhiteSpace(categoryId))
                throw new DomainException("Item category is required.");

            var list = prices.ToList();
            if (list.Count == 0)
                throw new DomainException("Item needs at least one price.");
            if (list.Select(p => p.Currency).Distinct().Count() != list.Count)
                throw new DomainException("Item has duplicate price currencies.");

            Id = id;
            Name = name;
            CategoryId = categoryId;
            ImageUrl = imageUrl;
            _prices = list;
        }

        public ItemPrice GetPrice(Currency currency)
        {
            return _prices.FirstOrDefault(p => p.Currency == currency)
                ?? throw new DomainException($"Item '{Name}' cannot be bought for {currency}.");
        }
    }
}
