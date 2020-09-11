using System;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace CardApplication.Domain.Models
{
    public class Encryption
    {
        public static string Encrypt(string cvc, string salt)
        {
            var ascii = Encoding.ASCII;
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: cvc,
                salt: ascii.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}