using Isopoh.Cryptography.Argon2;
using Jim.Core.Encryption.Models;
using System.Text;

namespace Jim.Core.Encryption.Service
{
    public static class Argon2ConfigGenerator
    {
        public static Argon2Config ToConfig(this Argon2EncryptionOptions options, string password, byte[] salt)
        {
            var config = new Argon2Config();
            config.Password = Encoding.UTF8.GetBytes(password);
            config.Salt = salt;
            config.Secret = Encoding.UTF8.GetBytes(options.Secret);

            return config;
        }
    }
}
