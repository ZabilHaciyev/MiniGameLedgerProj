using Ledger.Domain;
using Ledger.Infrastructure;
using Ledger.Infrastructure.Persistence;
using Ledger_Application.Abstractions;
using Ledger_Application.Accounts;
using Ledger_Application.Items;
using Ledger_Application.Players;
using Ledger_Application.Transactions.PostTransaction;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<LedgerDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("Default"),
        npgsql => npgsql.MigrationsAssembly("Ledger.Infrastructure")));
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICommandHandler<CreateAccountCommand, Guid>, CreateAccountHandler>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ICommandHandler<PostTransactionCommand, Guid>, PostTransactionHandler>();
builder.Services.AddScoped<ILedgerReadRepository, LedgerReadRepository>();
builder.Services.AddScoped<IQueryHandler<GetAccountBalanceQuery, AccountBalanceDto>, GetAccountBalanceHandler>();
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ICommandHandler<BuyItemCommand, Guid>, BuyItemHandler>();
builder.Services.AddScoped<ICommandHandler<OpenPlayerCommand, PlayerDto>, OpenPlayerHandler>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LedgerDbContext>();
    await db.Database.MigrateAsync();
    await DbSeeder.SeedAsync(db);
}
app.Use(async (ctx, next) =>
{
    try
    {
        await next();
    }
    catch (DomainException ex)
    {
        ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
        await ctx.Response.WriteAsJsonAsync(new { error = ex.Message });
    }
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Ledger API");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();

app.Run();
