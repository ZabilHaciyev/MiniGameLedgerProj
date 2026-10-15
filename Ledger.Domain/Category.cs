using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public sealed class Category
    {
        public string Id { get; private set; } = default!;
        public string Name { get; private set; } = default!;

        private Category() { }   

        public Category(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new DomainException("Category id is required.");
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Category name is required.");

            Id = id;
            Name = name;
        }
    }
}
