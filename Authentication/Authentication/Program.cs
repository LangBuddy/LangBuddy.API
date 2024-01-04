using Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile(path: $"appsettings.{env}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Debug")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
