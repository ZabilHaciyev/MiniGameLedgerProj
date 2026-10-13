using Ledger_Application.Abstractions;
using Ledger_Application.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace Ledger.Api.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        public sealed record CreateAccountRequest(string Name, string Currency);

        [HttpPost]
        public async Task<IActionResult> Create( CreateAccountRequest request, ICommandHandler<CreateAccountCommand, Guid> handler,CancellationToken ct)
        {
            var id = await handler.HandleAsync(new CreateAccountCommand(request.Name, request.Currency), ct);
            return Created($"/api/accounts/{id}", new { id });
        }

        [HttpGet("{id:guid}/balance")]
        public async Task<IActionResult> GetBalance( Guid id, IQueryHandler<GetAccountBalanceQuery, AccountBalanceDto> handler, CancellationToken ct)
        {
            var result = await handler.HandleAsync(new GetAccountBalanceQuery(id), ct);
            return Ok(result);
        }
    }
}
