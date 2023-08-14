namespace SP.SaltingPassword.Domain.Entity
{
    public class Account
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public byte[] Salt { get; private set; }
        public string Password { get; private set; }

        public static Account Create(string name, string email, string password)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                Password = password
            };
        }

        public void SetSaltHashPassword(byte[] salt, string hashPassword)
        {
            Salt = salt;
            Password = hashPassword;
        }
    }
}
