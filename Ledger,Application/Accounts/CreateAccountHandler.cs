using Ledger.Domain;
using Ledger_Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger_Application.Accounts
{

    public sealed class CreateAccountHandler(IAccountRepository accounts)
        : ICommandHandler<CreateAccountCommand, Guid>
    {
        public async Task<Guid> HandleAsync(CreateAccountCommand command, CancellationToken ct = default)
        {
            if (await accounts.NameExistsAsync(command.Name, ct))
                throw new DomainException($"Account '{command.Name}' already exists.");

            var account = new Account(Guid.NewGuid(), command.Name, new Currency(command.Currency));
            await accounts.AddAsync(account, ct);

            return account.Id;
        }
    }
}
