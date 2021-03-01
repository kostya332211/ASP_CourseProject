using System;
using System.Security.Cryptography;
using System.Text;

namespace Chess.Web.Helpers
{
    
    public class Sha512Hash
    {
        private const int SaltLength = 10;
        private static string CalculateHashInternal(string plainText)
        {
            var bytes = Encoding.Unicode.GetBytes(plainText);
            using var sha512 = SHA512.Create();
            var hashed = sha512.ComputeHash(bytes);
            return Encoding.Unicode.GetString(hashed);
        }

        public static string ComputeHash(string password, string salt)
        {
            if (salt == null) throw new ArgumentNullException(nameof(salt), "Salt can not be null");
            if (password == null) throw new ArgumentNullException(nameof(password), "Password can not be null");
            return CalculateHashInternal(password + salt);
        }

        public static string GenerateSalt()
        {
            var str = new StringBuilder();
            var random = new Random();

            for (var i = 0; i < SaltLength; i++)
            {
                var x = random.NextDouble();
                var z = Convert.ToInt32(Math.Floor(25 * x));
                var letter = Convert.ToChar(z + 65);
                str.Append(letter);
            }

            return str.ToString();
        }
    }
}
