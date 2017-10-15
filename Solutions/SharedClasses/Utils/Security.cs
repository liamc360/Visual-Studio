using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SharedClasses.Utils
{
    
    public class Security
    {
        private const int SALT_LENGTH = 24;
        private const int HASH_BYTES = 18;
        private const int PBKDF2_ITERATIONS = 10000;

        public static string HashPassword(string password, string salt)
        {
            byte[] hash = PBKDF2(password, Convert.FromBase64String(salt), PBKDF2_ITERATIONS, HASH_BYTES);
            return Convert.ToBase64String(hash);
        }

        public static string GenerateSalt(int size = SALT_LENGTH)
        {
            byte[] salt = new byte[SALT_LENGTH];

            using (RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider())
            {
                csprng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }
        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt))
            {
                pbkdf2.IterationCount = iterations;
                return pbkdf2.GetBytes(outputBytes);
            }
        }
    }
}
