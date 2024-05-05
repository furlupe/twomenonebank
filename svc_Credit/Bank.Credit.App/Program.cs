using System.Reflection;
using Bank.Auth.Common.Extensions;
using Bank.Auth.Common.Policies.Handlers;
using Bank.Auth.Http.AuthClient;
using Bank.Common;
using Bank.Common.DateTimeProvider;
using Bank.Common.Extensions;
using Bank.Common.Middlewares;
using Bank.Common.Middlewares.Conditional500Error;
using Bank.Common.Middlewares.Tracing;
using Bank.Core.Http.Client;
using Bank.Credit.App.Services;
using Bank.Credit.App.Setup;
using Bank.Credit.Persistance;
using Bank.Credit.Persistance.Extensions;
using Bank.Idempotency.Extensions.Swagger;
using Bank.Idempotency.Middlewares;
using Bank.Logging.Extensions;
using Bank.TransactionsGateway.Http.Client;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
    o.AddAuth().AddIdempotencyHeader().UseXmlComments(Assembly.GetExecutingAssembly())
);

builder.AddConfiguration();

builder
    .Services.AddTransient<TariffService>()
    .AddScoped<IUserService, AuthHandlerUserService>()
    .AddTransient<CreditService>()
    .AddTransient<CreditUserService>()
    .AddScoped<IDateTimeProvider, DateTimeProvider>();

builder.AddPersistance();
builder.ConfigureAuth().AddUserCreationPolicy();
builder.ConfigureHangfire();
builder.AddAuthClient().AddCoreClient().AddTransactionsClient();

builder.AddLogging();

builder.AddIdempotency<IdempotentActionService>();

var app = builder.Build();

app.UseTracing();
app.UseLogging();

await app.UsePersistance();

app.UseMiddleware<ErrorHandlingMiddleware>();
if (app.TransientErrorsEnabled())
{
    app.UseConditional500ErrorMiddleware();
}

app.UseIdempotency();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard();
app.SetupCreditJobs();

app.MapControllers();

app.Run();
