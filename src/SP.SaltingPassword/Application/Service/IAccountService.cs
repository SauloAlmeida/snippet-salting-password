using SP.SaltingPassword.Domain.Entity;

namespace SP.SaltingPassword.Application.Service
{
    public interface IAccountService
    {
        void Save(Account input);
        bool Login(string email, string password);
    }
}
