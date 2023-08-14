using FluentAssertions;
using SP.SaltingPassword.Application.Service;
using SP.SaltingPassword.Tests.Common.Account;

namespace SP.SaltingPassword.Tests.Application.Service.Account
{
    public class Account
    {
        readonly AccountFixture _fixture = new();
        private readonly IAccountService _accountService;

        public Account()
        {
            ISecurityService _securityService = new SecurityService();
            _accountService = new AccountService(_securityService);
        }

        [Fact(DisplayName = nameof(Save_ValidInputs_ReturnsValid))]
        public void Save_ValidInputs_ReturnsValid()
        {
            var accountInput = _fixture.CreateAccount();

            Action output = () => _accountService.Save(accountInput);

            output.Should().NotThrow();
        }

        [Fact]
        public void Login_ValidInputs_ReturnsValid()
        {
            var accountInput = _fixture.CreateAccount();
            var currentPassword = accountInput.Password;
            _accountService.Save(accountInput);

            var output = _accountService.Login(accountInput.Email, currentPassword);

            output.Should().BeTrue();
        }
    }
}
