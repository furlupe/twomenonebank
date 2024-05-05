using Bank.Common;
using Bank.Common.Extensions;
using Bank.Common.Middlewares.Conditional500Error;
using Bank.Monitoring.App.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddConfiguration().AddElasticClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.TransientErrorsEnabled())
{
    app.UseConditional500ErrorMiddleware();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
