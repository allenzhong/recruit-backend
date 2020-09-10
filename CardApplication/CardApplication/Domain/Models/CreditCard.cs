using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace CardApplication.Domain.Models
{
    public class CreditCard
    {
        private string cvc = "";
        public long Id { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }

        public string Cvc
        {
            get
            {
                return cvc;
            }
            set
            {
                Encoding ascii = Encoding.ASCII;  
                Encoding unicode = Encoding.Unicode;
                cvc = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: value,
                    salt: unicode.GetBytes(Salt), 
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
            }
        }

        public string Salt { get; set; }
        public DateTime ExpiryDate { get; set; }

        public static string GenerateSalt(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}