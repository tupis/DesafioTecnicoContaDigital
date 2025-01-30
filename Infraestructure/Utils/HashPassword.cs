using System.Security.Cryptography;

namespace Infraestructure.Utils
{
    public class HashPassword
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA256;
        private const char Delimeter = ';';


        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);    
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, hashAlgorithm,KeySize);
            return string.Join(Delimeter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public bool Verify(string passwordHash, string inputPassword)
        {
            var elements = passwordHash.Split(Delimeter);
            var salt = Convert.FromBase64String(elements[0]);
            var hash = Convert.FromBase64String(elements[1]);

            var hashInput  = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iterations, hashAlgorithm, KeySize);

            return CryptographicOperations.FixedTimeEquals(hash, hashInput);
        }
    }
}
