using Microsoft.AspNetCore.Mvc;
using SP.SaltingPassword.Application.Service;
using SP.SaltingPassword.Presentation.Models;

namespace SP.SaltingPassword.Presentation.Routes.Account
{
    public static class AccountRoutes
    {
        public static RouteHandlerBuilder CreateAccount(this WebApplication app)
        {
            return app.MapPost("/account", ([FromServices] IAccountService service, [FromBody] AccountInput input) =>
            {
                var newAccount = Domain.Entity.Account.Create(input.Name, input.Email, input.Password);

                service.Save(newAccount);

                return Results.NoContent();
            })
            .WithOpenApi();
        }

        public static RouteHandlerBuilder Login(this WebApplication app)
        {
            return app.MapPost("/account/login", ([FromServices] IAccountService service, [FromBody] LoginInput input) =>
            {
                bool output = service.Login(input.Email, input.Password);

                return output ? Results.Ok() : Results.BadRequest();
            })
            .WithOpenApi();
        }
    }
}
