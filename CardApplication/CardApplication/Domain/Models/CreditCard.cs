using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardApplication.Domain.Models
{
    [Table("CreditCards")]
    public class CreditCard
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string Cvc { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}