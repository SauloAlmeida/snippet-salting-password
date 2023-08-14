using Carter;
using SP.SaltingPassword.Application.Service;

var builder = WebApplication.CreateBuilder(args);

AddDocumentation(builder);

AddServices(builder);

AddRoutes(builder);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCarter();

await app.RunAsync();

static void AddDocumentation(WebApplicationBuilder builder)
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

static void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<ISecurityService, SecurityService>();
    builder.Services.AddSingleton<IAccountService, AccountService>();
}

static void AddRoutes(WebApplicationBuilder builder)
{
    builder.Services.AddCarter();
}
