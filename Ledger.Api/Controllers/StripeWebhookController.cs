using Ledger.Domain;
using Ledger_Application.Abstractions;
using Ledger_Application.Transactions.PostTransaction;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace Ledger.Api.Controllers
{
    [ApiController]
    [Route("api/stripe")]
    public class StripeWebhookController(IConfiguration config, IPackageRepository packages, ICommandHandler<PostTransactionCommand, Guid> postTransaction) : ControllerBase
    {
        [HttpPost("webhook")]
        public async Task<IActionResult> Handle(CancellationToken ct)
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync(ct);

            Event stripeEvent;
            try
            {
                stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    config["Stripe:WebhookSecret"],
                    throwOnApiVersionMismatch: false);
            }
            catch (StripeException)
            {
                return BadRequest(new { error = "Invalid signature." });
            }

            if (stripeEvent.Type != "checkout.session.completed")
                return Ok();

            var session = (Session)stripeEvent.Data.Object;

            if (session.PaymentStatus != "paid")
                return Ok();

            var usdAccountId = Guid.Parse(session.Metadata["usdAccountId"]);
            var rewardAccountId = Guid.Parse(session.Metadata["rewardAccountId"]);
            var package = await packages.GetAsync(session.Metadata["packageId"], ct)
                ?? throw new DomainException("Unknown package.");

            var usd = "USD";
            var entries = new List<EntryDto>
        {
            new(usdAccountId, -package.PriceUsd, usd),
            new(SystemAccounts.BankFor(new Currency(usd)), package.PriceUsd, usd),
            new(SystemAccounts.BankFor(package.RewardCurrency), -package.RewardAmount, package.RewardCurrency.Code),
            new(rewardAccountId, package.RewardAmount, package.RewardCurrency.Code)
        };

            await postTransaction.HandleAsync(new PostTransactionCommand(entries, stripeEvent.Id), ct);
            return Ok();
        }
    }
}
