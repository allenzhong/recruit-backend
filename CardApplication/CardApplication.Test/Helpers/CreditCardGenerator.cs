using System;
using Bogus;
using CardApplication.Application.Models;
using CardApplication.Domain.Models;

namespace CardApplication.Test.Helpers
{
    public static class CreditCardGenerator
    {
        public static Faker<CardInput> CreateValidCardInputFaker()
        {
            var faker = new Faker<CardInput>()
                .RuleFor(u => u.Name, (f) => f.Name.LastName())
                .RuleFor(u => u.CardNumber, f => f.Finance.CreditCardNumber().Replace("-", ""))
                .RuleFor(u => u.Cvc, f => f.Finance.CreditCardCvv())
                .RuleFor(u => u.ExpiryDate, f => f.Date.Between(DateTime.Today, DateTime.Today.AddYears(3)).Date);
            return faker;
        }
        public static Faker<CreditCard> CreateValidCreditCardFaker()
        {
            var faker = new Faker<CreditCard>()
                .RuleFor(u => u.Name, (f) => f.Name.LastName())
                .RuleFor(u => u.CardNumber, f => f.Finance.CreditCardNumber().Replace("-", ""))
                .RuleFor(u => u.EncryptedCvc, f => Encryption.Encrypt(f.Finance.CreditCardCvv(), "salt"))
                .RuleFor(u => u.ExpiryDate, f => f.Date.Between(DateTime.Today, DateTime.Today.AddYears(3)).Date);
            return faker;
        }

        public static Faker<CreditCartOutput> CreateValidCreditCardOutputFaker()
        {
            var faker = new Faker<CreditCartOutput>()
                .RuleFor(u => u.Id, (f) => f.Random.Long())
                .RuleFor(u => u.Name, (f) => f.Name.LastName())
                .RuleFor(u => u.CardNumber, f => f.Finance.CreditCardNumber().Replace("-", ""))
                .RuleFor(u => u.ExpiryDate, f => f.Date.Between(DateTime.Today, DateTime.Today.AddYears(3)).Date);
            return faker; 
        }
    }
}