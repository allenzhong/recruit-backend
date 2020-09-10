using System;

namespace CardApplication.Domain.Models
{
    public class CreditCard
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string Cvc { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}