using Bank.Auth.Shared.Extensions;
using Bank.Auth.Shared.Policies.Handlers;
using Bank.Common.Extensions;
using Bank.Credit.App.Services;
using Bank.Credit.App.Setup;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddConfiguration();

builder
    .Services.AddTransient<TariffService>()
    .AddScoped<IUserService, AuthHandlerUserService>()
    .AddTransient<CreditService>()
    .AddTransient<CreditBackroundService>();

builder.AddPersistance();
builder.ConfigureAuth();
builder.ConfigureHangfire();

var app = builder.Build();

await app.UsePersistance();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard();
app.SetupCreditJobs();

app.MapControllers();

app.Run();
