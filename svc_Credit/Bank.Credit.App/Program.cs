using System.Reflection;
using Bank.Auth.Common.Extensions;
using Bank.Auth.Common.Policies.Handlers;
using Bank.Common.Extensions;
using Bank.Credit.App.Services;
using Bank.Credit.App.Setup;
using Bank.Credit.Persistance.Extensions;
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
    .AddTransient<CreditBackroundService>();

builder.AddPersistance();
builder.ConfigureAuth(opt => opt.RegisterPolicies());
builder.ConfigureHangfire();

var app = builder.Build();

await app.UsePersistance();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard();
app.SetupCreditJobs();

app.MapControllers();

app.Run();
