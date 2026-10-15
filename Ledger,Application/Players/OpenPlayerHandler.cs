using Ledger.Domain;
using Ledger_Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ledger_Application.Players
{

    public sealed class OpenPlayerHandler(IAccountRepository accounts, ILedgerReadRepository ledger) : ICommandHandler<OpenPlayerCommand, PlayerDto>
    {
        private static readonly string[] Currencies = ["USD", "DIA", "COI", "GLD"];

        public async Task<PlayerDto> HandleAsync(OpenPlayerCommand command, CancellationToken ct = default)
        {
            if (!Regex.IsMatch(command.Name ?? "", "^[a-zA-Z0-9_]{2,30}$"))
                throw new DomainException("Name must be 2-30 chars: letters, digits, underscore.");

            var result = new List<PlayerAccountDto>();
            foreach (var code in Currencies)
            {
                var accountName = $"{command.Name}:{code}";
                var account = await accounts.GetByNameAsync(accountName, ct);

                if (account is null)
                {
                    account = new Account(Guid.NewGuid(), accountName, new Currency(code));
                    await accounts.AddAsync(account, ct);
                }

                var balance = await ledger.GetBalanceAsync(account.Id, ct);
                result.Add(new PlayerAccountDto(account.Id, code, balance));
            }

            return new PlayerDto(command.Name, result);
        }
    }
}
