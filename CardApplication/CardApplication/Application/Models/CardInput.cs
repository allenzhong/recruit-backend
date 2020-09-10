using System;

namespace CardApplication.Application.Models
{
    public class CardInput
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string Cvc { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}