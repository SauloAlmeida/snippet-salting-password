using SP.SaltingPassword.Application.Service;
using SP.SaltingPassword.Presentation.Routes;

var builder = WebApplication.CreateBuilder(args);

AddDocumentation(builder);

AddServices(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

ConfigureRoutes.Setup(app);

app.Run();

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