using AutoFixture;
using FluentAssertions;
using SP.SaltingPassword.Domain.Entity;
using SP.SaltingPassword.Presentation.Models;

namespace SP.SaltingPassword.Tests.Domain.Entity
{
    public class AccountTests
    {
        readonly Fixture _fixture = new();

        [Fact(DisplayName = nameof(Instantiate))]
        public void Instantiate()
        {
             _fixture.Create<AccountInput>().Deconstruct(out string name, out string email, out string password);

            var newAccount = Account.Create(name, email, password);

            newAccount.Should().NotBeNull();
            newAccount.Name.Should().Be(name);
            newAccount.Email.Should().Be(email);
            newAccount.Password.Should().Be(password);
            newAccount.Salt.Should().BeNull();
        }

        [Fact(DisplayName = nameof(Instantiate_SetSaltHash))]
        public void Instantiate_SetSaltHash()
        {
            var account = _fixture.Create<Account>();

            var salt = _fixture.Create<byte[]>();
            var hashPassword = _fixture.Create<Guid>().ToString();

            account.SetSaltHashPassword(salt, hashPassword);

            account.Should().NotBeNull();
            account.Password.Should().Be(hashPassword);
            account.Salt.Should().Equal(salt);
        }
    }
}
