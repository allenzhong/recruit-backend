using System;
using FluentValidation;

namespace CardApplication.Application.Models
{
    public class CardModelValidator: AbstractValidator<CardInput>
    {
        public CardModelValidator()
        {
            RuleFor(c => c.CardNumber).CreditCard();
            RuleFor(c => c.Cvc).Matches(@"^[0-9]{3,4}$");
            RuleFor(c => c.Name).NotNull().MaximumLength(50);
            RuleFor(c => c.ExpiryDate).GreaterThanOrEqualTo(x => DateTime.Today);
        }
    }
}