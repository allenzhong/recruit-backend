using CardApplication.Domain.Models;
using Xunit;

namespace CardApplication.Test.Domain.Models
{          
    [Trait("Category", "Unittest")]
    public class CreditCardModelTests
    {
        [Fact]
        public void ShouldCvcNumberBeEncryptedWhenValueSet()
        {
            var creditCard = new CreditCard();

            var cvc = new Bogus.Faker().Finance.CreditCardCvv();
            creditCard.EncryptCvcCode(cvc, 5);

            Assert.NotEqual(cvc, creditCard.EncryptedCvc);
        }

    }
}