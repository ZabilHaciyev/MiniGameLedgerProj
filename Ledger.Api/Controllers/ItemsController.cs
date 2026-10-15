using Ledger_Application.Abstractions;
using Ledger_Application.Items;
using Microsoft.AspNetCore.Mvc;

namespace Ledger.Api.Controllers
{

    [ApiController]
    [Route("api/items")]
    public class ItemsController(IItemRepository items, ICategoryRepository categories) : ControllerBase
    {
        public sealed record BuyItemRequest(Guid PlayerAccountId, string Currency);

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] string? categoryId, CancellationToken ct)
        {
            var list = await items.ListAsync(ct);
            return Ok(list
                .Where(i => categoryId is null || i.CategoryId == categoryId)
                .Select(i => new
                {
                    i.Id,
                    i.Name,
                    i.CategoryId,
                    i.ImageUrl,
                    prices = i.Prices.Select(p => new { currency = p.Currency.Code, p.Amount })
                }));
        }

        [HttpGet("categories")]
        public async Task<IActionResult> Categories(CancellationToken ct)
        {
            var list = await categories.ListAsync(ct);
            return Ok(list.Select(c => new { c.Id, c.Name }));
        }

        [HttpPost("{id}/buy")]
        public async Task<IActionResult> Buy(
            string id,
            BuyItemRequest request,
            ICommandHandler<BuyItemCommand, Guid> handler,
            CancellationToken ct,
            [FromHeader(Name = "Idempotency-Key")] string? idempotencyKey = null)
        {
            var txId = await handler.HandleAsync(
                new BuyItemCommand(request.PlayerAccountId, id, request.Currency, idempotencyKey), ct);
            return Ok(new { transactionId = txId });
        }
    }
}
