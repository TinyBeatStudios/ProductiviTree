using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Security
{
    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            byte[] salt = new byte[16];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }

            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256))
            {
                byte[] hash = deriveBytes.GetBytes(32);

                return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
            }
        }

        private static bool FixedTimeEquals(byte[] left, byte[] right)
        {
            if (left == null || right == null)
            {
                return false;
            }
            int diff = left.Length ^ right.Length;
            int length = Math.Min(left.Length, right.Length);
           
            for (int i = 0; i < length; i++)
            {
                diff |= left[i] ^ right[i];
            }
            return diff == 0;
        }
        public static bool Verify(string password, string storedValue)
        {
            string[] parts = storedValue.Split(':');
            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] expectedHash = Convert.FromBase64String(parts[1]);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256))
            {
                byte[] actualHash = deriveBytes.GetBytes(32);
                return FixedTimeEquals(actualHash, expectedHash);
            }
        }
    }
}
