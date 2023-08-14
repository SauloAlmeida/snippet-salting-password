using AutoFixture;
using SP.SaltingPassword.Presentation.Models;

namespace SP.SaltingPassword.Tests.Common.Account
{
    internal class AccountFixture : Fixture
    {
        readonly Fixture _fixture = new();

        public SaltingPassword.Domain.Entity.Account CreateAccount()
        {
            _fixture.Create<AccountInput>().Deconstruct(out string name, out string email, out string password);
            return SaltingPassword.Domain.Entity.Account.Create(name, email, password);
        }
    }
}
