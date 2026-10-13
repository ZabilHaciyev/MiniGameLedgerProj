using Ledger_Application.Abstractions;
using Ledger_Application.Transactions.PostTransaction;
using Microsoft.AspNetCore.Mvc;

namespace Ledger.Api.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionsController : ControllerBase
    {
        public sealed record PostTransactionRequest(List<EntryDto> Entries);

        [HttpPost]
        public async Task<IActionResult> Post(
            PostTransactionRequest request,
            ICommandHandler<PostTransactionCommand, Guid> handler,
            CancellationToken ct)
        {
            var id = await handler.HandleAsync(new PostTransactionCommand(request.Entries), ct);
            return Created($"/api/transactions/{id}", new { id });
        }
    }
}
