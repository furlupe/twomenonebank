using Bank.Auth.App.Setup.Extensions;
using Bank.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddConfiguration();

builder.AddPersistance().ConfigureAuth().AddServices();

var app = builder.Build();
await app.UsePersistance();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// same as applying [Authorize] attribute to all controllers
app.MapControllers();

app.Run();
