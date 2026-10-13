using Ledger.Domain;
using Ledger_Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger_Application.Transactions.PostTransaction
{
    public sealed class PostTransactionHandler(
     IAccountRepository accounts,
     ITransactionRepository transactions)
     : ICommandHandler<PostTransactionCommand, Guid>
    {
        public async Task<Guid> HandleAsync(PostTransactionCommand command, CancellationToken ct = default)
        {
            var ids = command.Entries.Select(e => e.AccountId).Distinct().ToList();
            var found = await accounts.GetByIdsAsync(ids, ct);

            var missing = ids.Except(found.Select(a => a.Id)).ToList();
            if (missing.Count > 0)
                throw new DomainException($"Accounts not found: {string.Join(", ", missing)}");

            var entries = new List<Entry>();
            foreach (var dto in command.Entries)
            {
                var account = found.First(a => a.Id == dto.AccountId);
                var currency = new Currency(dto.Currency);

                if (currency != account.Currency)
                    throw new DomainException(
                        $"Entry currency {currency} does not match account '{account.Name}' currency {account.Currency}.");

                entries.Add(new Entry(account.Id, new Money(dto.Amount, currency)));
            }

            var tx = LedgerTransaction.Create(Guid.NewGuid(), DateTimeOffset.UtcNow, entries);
            await transactions.AddAsync(tx, ct);

            return tx.Id;
        }
    }
}
