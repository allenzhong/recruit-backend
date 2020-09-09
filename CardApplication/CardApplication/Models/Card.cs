using System;

namespace CardApplication.Models
{
    public class Card
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string Cvc { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}