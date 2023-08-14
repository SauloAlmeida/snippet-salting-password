using FluentAssertions;
using SP.SaltingPassword.Application.Service;
using SP.SaltingPassword.Tests.Common.Account;

namespace SP.SaltingPassword.Tests.Application.Service.Security
{
    public class SecurityServiceTests
    {
        readonly AccountFixture _fixture = new();
        private readonly ISecurityService _securityService = new SecurityService();


        [Fact(DisplayName = nameof(GenerateHash_ValidInputs_ReturnsHashSalt))]
        public void GenerateHash_ValidInputs_ReturnsHashSalt()
        {
            var accountInput = _fixture.CreateAccount();

            var output = _securityService.GenerateHash(accountInput.Password, out byte[] salt);

            output.Should().NotBeNull();
            output.Should().NotBe(accountInput.Password);
            salt.Should().NotBeNull().And.HaveCountGreaterThan(1);
        }

        [Fact(DisplayName = nameof(ValidateHash_ValidInputs_ReturnsValid))]
        public void ValidateHash_ValidInputs_ReturnsValid()
        {
            var accountInput = _fixture.CreateAccount();
            var hashPassword = _securityService.GenerateHash(accountInput.Password, out byte[] salt);

            var output = _securityService.ValidateHash(accountInput.Password, hashPassword, salt);

            output.Should().BeTrue();
        }

        [Fact(DisplayName = nameof(ValidateHash_InvalidInputs_ReturnsInvalid))]
        public void ValidateHash_InvalidInputs_ReturnsInvalid()
        {
            var accountInput = _fixture.CreateAccount();
            var hashPassword = _securityService.GenerateHash(accountInput.Password, out byte[] salt);

            var output = _securityService.ValidateHash(Guid.NewGuid().ToString(), hashPassword, salt);

            output.Should().BeFalse();
        }
    }
}
