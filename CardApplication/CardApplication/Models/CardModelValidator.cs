using System;
using FluentValidation;

namespace CardApplication.Models
{
    public class CardModelValidator: AbstractValidator<Card>
    {
        public CardModelValidator()
        {
            RuleFor(c => c.Id).NotNull();
            RuleFor(c => c.CardNumber).CreditCard();
            RuleFor(c => c.Cvc).Matches(@"^[0-9]{3,4}$");
            RuleFor(c => c.Name).NotNull().MaximumLength(50);
            RuleFor(c => c.ExpiryDate).GreaterThanOrEqualTo(x => DateTime.Today);
        }
    }
}