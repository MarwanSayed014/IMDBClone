using IMDBClone.Helpers.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using XSystem.Security.Cryptography;

namespace IMDBClone.Helpers
{
    public class PasswordManager : IPasswordManager
    {
        public async Task<string> Hash(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException();
            using (var sha256 = new SHA256Managed())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
