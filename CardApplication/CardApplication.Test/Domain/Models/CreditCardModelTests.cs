using CardApplication.Domain.Models;
using Xunit;

namespace CardApplication.Test.Domain.Models
{          
    [Trait("Category", "Unittest")]
    public class CreditCardModelTests
    {
        [Fact]
        public void ShouldGenerateRandomStringWithSpecifiedLength_WhenCallingGenerateSalt()
        {
            var salt = CreditCard.GenerateSalt(10);
            Assert.NotEmpty(salt);
            Assert.Equal(10, salt.Length);
        }
        
        [Fact]
        public void ShouldCvcNumberBeEncryptedWhenValueSet()
        {
            var creditCard = new CreditCard();

            var cvc = new Bogus.Faker().Finance.CreditCardCvv();
            creditCard.Salt = CreditCard.GenerateSalt(5);
            creditCard.Cvc = cvc;

            Assert.NotEqual(cvc, creditCard.Cvc);
        }

    }
}