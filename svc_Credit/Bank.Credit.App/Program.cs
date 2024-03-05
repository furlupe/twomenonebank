using Bank.Auth.Shared.Extensions;
using Bank.Auth.Shared.Policies.Handlers;
using Bank.Credit.App.Services;
using Bank.Credit.Persistance;
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
    .AddTransient<CreditService>();

builder.ConfigureAuth();

builder.Services.AddDbContext<BankCreditDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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

app.MapControllers();

app.Run();
