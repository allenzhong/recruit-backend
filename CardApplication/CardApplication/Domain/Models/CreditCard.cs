using System;
using System.Linq;

namespace CardApplication.Domain.Models
{
    public class CreditCard
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string EncryptedCvc { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CvcSalt { get; set; } = "";

        private string GenerateSalt(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void EncryptCvcCode(string cvc, int saltLength)
        {
            CvcSalt = GenerateSalt(saltLength);
            EncryptedCvc = Encryption.Encrypt(cvc, CvcSalt);
        }
    }
}