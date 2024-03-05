using Bank.Auth.Shared.Extensions;
using Bank.Auth.Shared.Policies.Handlers;
using Bank.Credit.App.Services;
using Bank.Credit.App.Setup;
using Bank.Credit.Persistance;
using Hangfire;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder
    .Services.AddTransient<TariffService>()
    .AddScoped<IUserService, AuthHandlerUserService>()
    .AddTransient<CreditService>()
    .AddTransient<CreditBackroundService>();

builder.Services.AddDbContext<BankCreditDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.ConfigureAuth();
builder.ConfigureHangfire();

var app = builder.Build();

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
