using System;
using System.Linq;
using Bogus;
using CardApplication.Models;
using Xunit;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace CardApplication.Test
{
    public class CardModelValidatorTests
    {
        [Fact]
        public void ShouldReturnValidWhenCardInfoIsValid()
        {
            var card = CreateValidFaker().Generate();
            
            var result = ValidateCard(card);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ShouldReturnInvalidWhenIdIsNull()
        {
            var card = CreateValidFaker() 
                .RuleFor(u => u.Id, f => Guid.Empty)
                .Generate();
            var result = ValidateCard(card);
            Assert.False(result.IsValid);
        }
        
        [Fact]
        public void ShouldReturnInvalidWhenNameIsNull()
        {
            var card = CreateValidFaker()
                .RuleFor(u => u.Name, (f) => null)
                .Generate();
            
            var result = ValidateCard(card);
            Assert.False(result.IsValid);
        }
        
        [Fact]
        public void ShouldReturnInvalidWhenLenghtOfNameGreaterThan50()
        {
            var card = CreateValidFaker()
                .RuleFor(u => u.Name, (f) => f.Random.String(51))
                .Generate();
            
            var result = ValidateCard(card);
            Assert.False(result.IsValid);
        }
        
        [Fact]
        public void ShouldReturnInvalidWhenCreditCarNumberIsInvalid()
        {
            var card = CreateValidFaker()
                .RuleFor(u => u.CardNumber, (f) => f.Random.String(11))
                .Generate();
            
            var result = ValidateCard(card);
            Assert.False(result.IsValid);
        }
        
        [Fact]
        public void ShouldReturnValidWhenCvcLengthIs3()
        {
            var card = CreateValidFaker()
                .RuleFor(u => u.Cvc, (f) => string.Join("", f.Random.Digits(3)))
                .Generate();
            
            var result = ValidateCard(card);
            Assert.True(result.IsValid);
        }
        [Fact]
        public void ShouldReturnValidWhenCvcLengthIs4()
        {
            var card = CreateValidFaker()
                .RuleFor(u => u.Cvc, (f) => string.Join("", f.Random.Digits(4)))
                .Generate();
            
            var result = ValidateCard(card);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ShouldReturnInvalidWhenCvcIsInvalid()
        {
            var card = CreateValidFaker()
                .RuleFor(u => u.Cvc, (f) => f.Random.String(3))
                .Generate();
            
            var result = ValidateCard(card);
            Assert.False(result.IsValid);
        }
        
        
        [Fact]
        public void ShouldReturnInvalidWhenCvcLengthGreaterThan4()
        {
            var card = CreateValidFaker()
                .RuleFor(u => u.Cvc, (f) => f.Random.String(5))
                .Generate();
            
            var result = ValidateCard(card);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void ShouldReturnInvalidWhenExpiryDateIsBeforeToday()
        {
            var card = CreateValidFaker()
                .RuleFor(u => u.ExpiryDate, (f) => f.Date.Past())
                .Generate();
            
            var result = ValidateCard(card);
            Assert.False(result.IsValid); 
        }
        
        [Fact]
        public void ShouldReturnValidWhenExpiryDateIsToday()
        {
            var card = CreateValidFaker()
                .RuleFor(u => u.ExpiryDate, (f) => DateTime.Today)
                .Generate();
            
            var result = ValidateCard(card);
            Assert.True(result.IsValid); 
        }
        
        private static Faker<Card> CreateValidFaker()
        {
            var faker = new Faker<Card>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Name, (f) => f.Name.FullName())
                .RuleFor(u => u.CardNumber, f => f.Finance.CreditCardNumber())
                .RuleFor(u => u.Cvc, f => f.Finance.CreditCardCvv())
                .RuleFor(u => u.ExpiryDate, f => f.Date.Between(DateTime.Today, DateTime.Today.AddYears(3)));
            return faker;
        }

        private static ValidationResult ValidateCard(Card card)
        {
            var cardValidator = new CardModelValidator();
            var result = cardValidator.Validate(card);
            return result;
        }

    }
}