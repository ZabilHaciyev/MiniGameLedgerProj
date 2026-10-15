using Ledger.Domain;
using Ledger_Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace Ledger.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class PurchasesController(IConfiguration config, IAccountRepository accounts, IPackageRepository packages) : ControllerBase
    {
        public sealed record CreatePurchaseRequest(Guid PlayerUsdAccountId, Guid PlayerRewardAccountId, string PackageId);

        [HttpGet("packages")]
        public async Task<IActionResult> ListPackages(CancellationToken ct)
        {
            var list = await packages.ListAsync(ct);
            return Ok(list.Select(p => new
            {
                p.Id,
                p.Name,
                p.PriceUsd,
                rewardCurrency = p.RewardCurrency.Code,
                p.RewardAmount
            }));
        }

        [HttpPost("purchases")]
        public async Task<IActionResult> Create(CreatePurchaseRequest request, CancellationToken ct)
        {
            var package = await packages.GetAsync(request.PackageId, ct)
                ?? throw new DomainException("Unknown package.");

            var usdAccount = await accounts.GetAsync(request.PlayerUsdAccountId, ct)
                ?? throw new DomainException("USD account not found.");

            var rewardAccount = await accounts.GetAsync(request.PlayerRewardAccountId, ct)
                ?? throw new DomainException("Reward account not found.");

            if (usdAccount.Currency.Code != "USD")
                throw new DomainException("Payment account must be in USD.");

            if (rewardAccount.Currency != package.RewardCurrency)
                throw new DomainException($"Reward account must be in {package.RewardCurrency}.");

            StripeConfiguration.ApiKey = config["Stripe:SecretKey"];

            var options = new SessionCreateOptions
            {
                Mode = "payment",
                UiMode = "embedded_page",
                ReturnUrl = "https://localhost:7000/?paid=1", 
                LineItems =
                [
                    new SessionLineItemOptions
                    {
                        Quantity = 1,
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            UnitAmount = (long)(package.PriceUsd * 100),
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = package.Name
                            }
                        }
                    }
                ],
                Metadata = new Dictionary<string, string>
                {
                    ["usdAccountId"] = usdAccount.Id.ToString(),
                    ["rewardAccountId"] = rewardAccount.Id.ToString(),
                    ["packageId"] = package.Id
                }
            };

            var session = await new SessionService().CreateAsync(options, cancellationToken: ct);
            return Ok(new
            {
                clientSecret = session.ClientSecret,
                publishableKey = config["Stripe:PublishableKey"]
            });
        }
    }
}