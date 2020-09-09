using System;
using Bogus;
using CardApplication.Application.Models;

namespace CardApplication.Test.Application.Models
{
    public class CardInputGenerator
    {
        public static Faker<CardInput> CreateValidFaker()
        {
            var faker = new Faker<CardInput>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Name, (f) => f.Name.FullName())
                .RuleFor(u => u.CardNumber, f => f.Finance.CreditCardNumber())
                .RuleFor(u => u.Cvc, f => f.Finance.CreditCardCvv())
                .RuleFor(u => u.ExpiryDate, f => f.Date.Between(DateTime.Today, DateTime.Today.AddYears(3)));
            return faker;
        }
    }
}