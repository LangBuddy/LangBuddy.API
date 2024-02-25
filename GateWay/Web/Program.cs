using Microsoft.OpenApi.Models;
using Service;
using Service.Middlewares;
using Service.Options;
using Web.Hubs;

var builder = WebApplication.CreateBuilder(args);
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

// Add services to the container.

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile(path: $"appsettings.{env}.json", optional: true)
.AddEnvironmentVariables()
.Build();

builder.Services.Configure<ApiOptions>(u => builder.Configuration.GetSection("ApiOptions").Bind(u));
    
builder.Services.AddServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSignalR();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

app.MapHub<ChatHub>("/api/private/chat-hub");
app.MapHub<MessagesHub>("/api/private/messages-hub");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Debug")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true) // allow any origin
        .AllowCredentials());

app.UseWhen(
    context => 
        context.Request.Path.StartsWithSegments("/api/Authentication/check-auth") || 
        context.Request.Path.StartsWithSegments("/api/private"),
    appBuilder =>
    {
        appBuilder.UseAuthenticationMiddleware();
    }
);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
