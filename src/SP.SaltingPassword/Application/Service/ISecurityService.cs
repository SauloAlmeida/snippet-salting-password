namespace SP.SaltingPassword.Application.Service
{
    public interface ISecurityService
    {
        string GenerateHash(string password, out byte[] salt);
        bool ValidateHash(string password, string hash, byte[] salt);
    }
}