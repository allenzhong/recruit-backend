using System;
using Bogus;
using CardApplication.Models;
using Xunit;

namespace CardApplication.Test
{
    public class CardModelValidatorTests
    {
        [Fact]
        public void ShouldReturnValidWhenCardInfoIsValid()
        {
            var card = new Faker<Card>()
                .RuleFor(u => u.Name, (f) => f.Name.FullName())
                .RuleFor(u => u.CardNumber, f => f.Finance.CreditCardNumber())
                .RuleFor(u => u.Cvc, f => f.Finance.CreditCardCvv())
                .RuleFor(u => u.ExpiryDate, f => f.Date.Between(DateTime.Today, DateTime.Today.AddYears(3)))
                .Generate();
            var cardValidator = new CardModelValidator();
            var result = cardValidator.Validate(card); 
            Assert.True(result.IsValid);
        }
    }
}