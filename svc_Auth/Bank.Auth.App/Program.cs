using System.Reflection;
using Bank.Auth.App.Setup.Extensions;
using Bank.Auth.Common.Extensions;
using Bank.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMvc(o => o.EnableEndpointRouting = false);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.AddAuth().UseXmlComments(Assembly.GetExecutingAssembly()));

builder.AddConfiguration().AddAuth().AddPersistance();

var app = builder.Build();
await app.UsePersistance();

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
