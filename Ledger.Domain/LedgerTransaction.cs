using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{

   
    public sealed class LedgerTransaction
    {
        private readonly List<Entry> _entries = [];

        public Guid Id { get; }
        public DateTimeOffset CreatedAt { get; }
        public IReadOnlyList<Entry> Entries => _entries;

        private LedgerTransaction() { }                 

        private LedgerTransaction(Guid id, DateTimeOffset createdAt, List<Entry> entries)
        {
            Id = id;
            CreatedAt = createdAt;
            _entries = entries;
        }
        public static LedgerTransaction Create(Guid id, DateTimeOffset createdAt, IEnumerable<Entry> entries)
        {
            var list = entries.ToList();

            if (list.Count < 2)
                throw new DomainException("A transaction needs at least two entries.");

            var unbalanced = list
                .GroupBy(e => e.Amount.Currency)
                .Where(g => g.Sum(e => e.Amount.Amount) != 0);

            if (unbalanced.Any())
                throw new DomainException("Entries must sum to zero in every currency.");

            return new LedgerTransaction(id, createdAt, list);
        }
    }
}
