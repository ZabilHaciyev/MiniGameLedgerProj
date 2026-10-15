using Ledger.Domain;
using Ledger_Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger_Application.Items
{

    public sealed class BuyItemHandler( IItemRepository items, IAccountRepository accounts, ILedgerReadRepository ledger,ITransactionRepository transactions): ICommandHandler<BuyItemCommand, Guid>
    {
        public async Task<Guid> HandleAsync(BuyItemCommand command, CancellationToken ct = default)
        {
            if (command.IdempotencyKey is not null)
            {
                var existing = await transactions.FindIdByKeyAsync(command.IdempotencyKey, ct);
                if (existing is not null)
                    return existing.Value;
            }

            var item = await items.GetAsync(command.ItemId, ct) ?? throw new DomainException("Item not found.");

            var player = await accounts.GetAsync(command.PlayerAccountId, ct) ?? throw new DomainException("Player account not found.");

            var currency = new Currency(command.Currency);
            var price = item.GetPrice(currency);

            if (player.Currency != currency) throw new DomainException($"Account is in {player.Currency}, but you chose to pay in {currency}.");

            var balance = await ledger.GetBalanceAsync(player.Id, ct);
            if (balance < price.Amount) throw new DomainException("Insufficient funds.");

            var tx = LedgerTransaction.Create(
                Guid.NewGuid(),
                DateTimeOffset.UtcNow,
                [
                    new Entry(player.Id, new Money(-price.Amount, currency)),
                new Entry(SystemAccounts.BankFor(currency), new Money(price.Amount, currency))
                ],
                command.IdempotencyKey);

            await transactions.AddAsync(tx, ct);
            return tx.Id;
        }
    }
}
