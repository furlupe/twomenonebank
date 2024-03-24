using Bank.Core.App.Controllers;
using Bank.Core.App.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();
builder.AddFrontlineServices();

// TODO: remove
builder.Services.AddHttpClient<TestClient>();

var app = builder.Build();

await app.UseApplicationServices();
app.UseFrontlineServices();

app.Run();
