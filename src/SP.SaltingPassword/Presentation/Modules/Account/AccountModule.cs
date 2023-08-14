using Carter;
using Microsoft.AspNetCore.Mvc;
using SP.SaltingPassword.Application.Service;
using SP.SaltingPassword.Presentation.Models;

namespace SP.SaltingPassword.Presentation.Modules.Account
{
    public class AccountModule : CarterModule
    {
        const string BASE_PATH = "/account";

        public AccountModule() 
            : base(BASE_PATH)
        {
            IncludeInOpenApi();
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/", ([FromServices] IAccountService service, [FromBody] AccountInput input) =>
            {
                var newAccount = Domain.Entity.Account.Create(input.Name, input.Email, input.Password);

                service.Save(newAccount);

                return Results.NoContent();
            });


            app.MapPost("/login", ([FromServices] IAccountService service, [FromBody] LoginInput input) =>
            {
                bool output = service.Login(input.Email, input.Password);

                return output ? Results.Ok() : Results.BadRequest();
            });
        }
    }
}
