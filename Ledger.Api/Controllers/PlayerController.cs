using Ledger_Application.Abstractions;
using Ledger_Application.Players;
using Microsoft.AspNetCore.Mvc;

namespace Ledger.Api.Controllers
{
    [ApiController]
    [Route("api/players")]
    public class PlayersController : ControllerBase
    {
        public sealed record OpenPlayerRequest(string Name);

        [HttpPost("open")]
        public async Task<IActionResult> Open(OpenPlayerRequest request,ICommandHandler<OpenPlayerCommand, PlayerDto> handler,CancellationToken ct)
        {
            return Ok(await handler.HandleAsync(new OpenPlayerCommand(request.Name), ct));
        }
    }
}
