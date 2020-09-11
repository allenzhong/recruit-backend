using System;

namespace CardApplication.Application.Models
{
    public class CreditCartOutput
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; } 
    }
}