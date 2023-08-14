using SP.SaltingPassword.Presentation.Routes.Account;

namespace SP.SaltingPassword.Presentation.Routes
{
    public static class ConfigureRoutes
    {
        public static void Setup(WebApplication app)
        {
            app.CreateAccount();
            app.Login();
        }
    }
}
