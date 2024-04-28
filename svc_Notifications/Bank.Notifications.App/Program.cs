using Bank.Auth.Common.Extensions;
using Bank.Common;
using Bank.Common.Extensions;
using Bank.Common.Middlewares;
using Bank.Common.Middlewares.Conditional500Error;
using Bank.Common.Middlewares.Tracing;
using Bank.Logging.Extensions;
using Bank.Notifications.App.Setup;
using Bank.Notifications.Persistence.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.AddAuth().UseXmlComments(Assembly.GetExecutingAssembly()));

builder.AddConfiguration();

builder.AddPersistence();
builder.ConfigureAuth();

builder.AddNotifications();

builder.AddLogging();

var app = builder.Build();

app.UseTracing();
app.UseLogging();

await app.UsePersistence();

app.UseMiddleware<ErrorHandlingMiddleware>();
if (app.TransientErrorsEnabled())
{
    app.UseConditional500ErrorMiddleware();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
