using System;
using CardApplication.Application.Models;
using CardApplication.Test.Helpers;
using Xunit;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace CardApplication.Test.Application.Models
{
    [Trait("Category", "Unittest")]
    public class CardInputValidatorTests
    {
        [Fact]
        public void ShouldReturnValidWhenCardInfoIsValid()
        {
            var card = CreditCardGenerator.CreateValidCardInputFaker().Generate();
            
            var result = ValidateCard(card);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ShouldReturnInvalidWhenNameIsNull()
        {
            var card = CreditCardGenerator.CreateValidCardInputFaker()
                .RuleFor(u => u.Name, (f) => null)
                .Generate();
            
            var result = ValidateCard(card);
            Assert.False(result.IsValid);
        }
        
        [Fact]
        public void ShouldReturnInvalidWhenLenghtOfNameGreaterThan50()
        {
            var card = CreditCardGenerator.CreateValidCardInputFaker()
                .RuleFor(u => u.Name, (f) => f.Random.String(51))
                .Generate();
            
            var result = ValidateCard(card);
            Assert.False(result.IsValid);
        }
        
        [Fact]
        public void ShouldReturnInvalidWhenCreditCarNumberIsInvalid()
        {
            var card = CreditCardGenerator.CreateValidCardInputFaker()
                .RuleFor(u => u.CardNumber, (f) => f.Random.String(11))
                .Generate();
            
            var result = ValidateCard(card);
            Assert.False(result.IsValid);
        }
        
        [Fact]
        public void ShouldReturnValidWhenCvcLengthIs3()
        {
            var card = CreditCardGenerator.CreateValidCardInputFaker()
                .RuleFor(u => u.Cvc, (f) => string.Join("", f.Random.Digits(3)))
                .Generate();
            
            var result = ValidateCard(card);
            Assert.True(result.IsValid);
        }
        [Fact]
        public void ShouldReturnValidWhenCvcLengthIs4()
        {
            var card = CreditCardGenerator.CreateValidCardInputFaker()
                .RuleFor(u => u.Cvc, (f) => string.Join("", f.Random.Digits(4)))
                .Generate();
            
            var result = ValidateCard(card);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ShouldReturnInvalidWhenCvcIsInvalid()
        {
            var card = CreditCardGenerator.CreateValidCardInputFaker()
                .RuleFor(u => u.Cvc, (f) => f.Random.String(3))
                .Generate();
            
            var result = ValidateCard(card);
            Assert.False(result.IsValid);
        }
        
        
        [Fact]
        public void ShouldReturnInvalidWhenCvcLengthGreaterThan4()
        {
            var card = CreditCardGenerator.CreateValidCardInputFaker()
                .RuleFor(u => u.Cvc, (f) => f.Random.String(5))
                .Generate();
            
            var result = ValidateCard(card);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void ShouldReturnInvalidWhenExpiryDateIsBeforeToday()
        {
            var card = CreditCardGenerator.CreateValidCardInputFaker()
                .RuleFor(u => u.ExpiryDate, (f) => f.Date.Past())
                .Generate();
            
            var result = ValidateCard(card);
            Assert.False(result.IsValid); 
        }
        
        [Fact]
        public void ShouldReturnValidWhenExpiryDateIsToday()
        {
            var card = CreditCardGenerator.CreateValidCardInputFaker()
                .RuleFor(u => u.ExpiryDate, (f) => DateTime.Today)
                .Generate();
            
            var result = ValidateCard(card);
            Assert.True(result.IsValid); 
        }

        private static ValidationResult ValidateCard(CardInput cardInput)
        {
            var cardValidator = new CardModelValidator();
            var result = cardValidator.Validate(cardInput);
            return result;
        }

    }
}