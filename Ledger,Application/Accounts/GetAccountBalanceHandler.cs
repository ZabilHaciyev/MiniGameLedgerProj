using Ledger.Domain;
using Ledger_Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger_Application.Accounts
{

    public sealed class GetAccountBalanceHandler(
        IAccountRepository accounts,
        ILedgerReadRepository ledger)
        : IQueryHandler<GetAccountBalanceQuery, AccountBalanceDto>
    {
        public async Task<AccountBalanceDto> HandleAsync(GetAccountBalanceQuery query, CancellationToken ct = default)
        {
            var account = await accounts.GetAsync(query.AccountId, ct)
                ?? throw new DomainException("Account not found.");

            var balance = await ledger.GetBalanceAsync(query.AccountId, ct);

            return new AccountBalanceDto(account.Id, account.Name, account.Currency.Code, balance);
        }
    }
}
