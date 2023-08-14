using SP.SaltingPassword.Domain.Entity;

namespace SP.SaltingPassword.Application.Service
{
    public class AccountService : IAccountService
    {
        private readonly ISecurityService _securityService;
        private readonly IList<Account> _accounts;

        public AccountService(ISecurityService securityService)
        {
            _accounts = new List<Account>();
            _securityService = securityService;
        }

        public void Save(Account input)
        {
            var hashPassword = _securityService.GenerateHash(input.Password, out byte[] salt);

            input.SetSaltHashPassword(salt, hashPassword);

            _accounts.Add(input);
        }

        public bool Login(string email, string password)
        {
            var account = _accounts.FirstOrDefault(w => w.Email.Contains(email, StringComparison.OrdinalIgnoreCase), null);

            if (account == null) return false;

            return _securityService.ValidateHash(password, account.Password, account.Salt);
        }
    }
}
