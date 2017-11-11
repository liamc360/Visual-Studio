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

        public static string Get_HASH_SHA512(string password, string username, byte[] salt)
        {
            try
            {
                //required NameSpace: using System.Text;
                //Plain Text in Byte
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(password + username);

                //Plain Text + SALT Key in Byte
                byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + salt.Length];

                for (int i = 0; i < plainTextBytes.Length; i++)
                {
                    plainTextWithSaltBytes[i] = plainTextBytes[i];
                }

                for (int i = 0; i < salt.Length; i++)
                {
                    plainTextWithSaltBytes[plainTextBytes.Length + i] = salt[i];
                }

                HashAlgorithm hash = new SHA512Managed();
                byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);
                byte[] hashWithSaltBytes = new byte[hashBytes.Length + salt.Length];

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    hashWithSaltBytes[i] = hashBytes[i];
                }

                for (int i = 0; i < salt.Length; i++)
                {
                    hashWithSaltBytes[hashBytes.Length + i] = salt[i];
                }

                return Convert.ToBase64String(hashWithSaltBytes);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
