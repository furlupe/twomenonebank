using System.Reflection;
using Bank.Auth.Common.Extensions;
using Bank.Auth.Common.Policies.Handlers;
using Bank.Auth.Http.AuthClient;
using Bank.Common.DateTimeProvider;
using Bank.Common.Extensions;
using Bank.Common.Middlewares;
using Bank.Core.Http.Client;
using Bank.Credit.App.Services;
using Bank.Credit.App.Setup;
using Bank.Credit.Persistance.Extensions;
using Bank.TransactionsGateway.Http.Client;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.AddAuth().UseXmlComments(Assembly.GetExecutingAssembly()));

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
builder
    .AddAuthClient()
    .AddCoreClient()
    .AddTransactionsClient();

var app = builder.Build();

await app.UsePersistance();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard();
app.SetupCreditJobs();

app.MapControllers();

app.Run();
