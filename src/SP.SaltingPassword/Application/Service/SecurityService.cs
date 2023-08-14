using System.Security.Cryptography;
using System.Text;

namespace SP.SaltingPassword.Application.Service
{
    public class SecurityService : ISecurityService
    {
        private const int KEY_SIZE = 64;
        private const int ITERATIONS = 350000;
        private static HashAlgorithmName HashAlgorithm => HashAlgorithmName.SHA512;

        public string GenerateHash(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(KEY_SIZE);

            var passwordBytes = Encoding.UTF8.GetBytes(password);

            var hash = Rfc2898DeriveBytes.Pbkdf2(passwordBytes, 
                                                 salt, 
                                                 ITERATIONS,
                                                 HashAlgorithm, 
                                                 KEY_SIZE);

            return Convert.ToHexString(hash);
        }

        public bool ValidateHash(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, ITERATIONS, HashAlgorithm, KEY_SIZE);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
