using Consumer.Consumers;
using Consumer.Options;
using Consumer.Services.HttpService;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile(path: $"appsettings.{env}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

// Add services to the container.
builder.Services.Configure<RabbitMqConfig>(u => builder.Configuration.GetSection("RabbitMqConfig").Bind(u));
builder.Services.Configure<ApiOptions>(u => builder.Configuration.GetSection("ApiOptions").Bind(u));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IHttpService, HttpService>();
builder.Services.AddHostedService<EmailSendConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Debug")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
