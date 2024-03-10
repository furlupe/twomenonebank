using Bank.Core.App.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();
builder.AddFrontlineServices();

var app = builder.Build();

await app.UseApplicationServices();
app.UseFrontlineServices();

app.Run();
