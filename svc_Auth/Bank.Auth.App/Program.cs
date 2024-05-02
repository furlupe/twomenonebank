using System.Reflection;
using Bank.Auth.App.Setup.Extensions;
using Bank.Auth.Common.Extensions;
using Bank.Auth.Http.AuthClient;
using Bank.Common;
using Bank.Common.DateTimeProvider;
using Bank.Common.Extensions;
using Bank.Common.Middlewares;
using Bank.Common.Middlewares.Conditional500Error;
using Bank.Common.Middlewares.Tracing;
using Bank.Logging.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMvc(o => o.EnableEndpointRouting = false);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.AddAuth().UseXmlComments(Assembly.GetExecutingAssembly()));

builder
    .AddConfiguration()
    .AddLogging()
    .AddAuth()
    .AddPersistance()
    .AddAuthClient();

builder.Services.AddScoped<IDateTimeProvider, DateTimeProvider>();

var app = builder.Build();

app.UseTracing();
app.UseLogging();

await app.UsePersistance();

app.UseMiddleware<ErrorHandlingMiddleware>();
if (app.TransientErrorsEnabled())
{
    app.UseConditional500ErrorMiddleware(_ => true);
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// same as applying [Authorize] attribute to all controllers
app.MapControllers();
app.UseMvc();

app.Run();
