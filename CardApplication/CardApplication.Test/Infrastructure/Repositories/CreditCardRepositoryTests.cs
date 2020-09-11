using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CardApplication.Exceptions;
using CardApplication.Infrastructure.Repositories;
using CardApplication.Test.Helpers;
using Dapper;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CardApplication.Test.Infrastructure.Repositories
{
    [Trait("Category", "DbTest")]
    public class CreditCardRepositoryTests: SqlDbTestBase
    {
        private CreditCardRepository _creditCardRepository;

        public CreditCardRepositoryTests()
        {
            var logger = new Mock<ILogger<CreditCardRepository>>();
            _creditCardRepository = new CreditCardRepository(Connection, logger.Object);
        }

        [Fact]
        public async Task GivenRecordDoesNotExistShouldCreateRecord()
        {
            await ResetDatabase();
            var creditCard = CreditCardGenerator.CreateValidCreditCardFaker().Generate();
            await _creditCardRepository.Create(creditCard);

            var result = await Connection.QueryFirstAsync(
                @"SELECT * FROM CreditCards where CardNumber=@CardNumber",
                new { creditCard.CardNumber});
            
            Assert.Equal(creditCard.Name, result.Name);
            Assert.Equal(creditCard.CardNumber, result.CardNumber);
            Assert.Equal(creditCard.Cvc, result.Cvc);
            Assert.Equal(creditCard.CvcSalt, result.CvcSalt);
            Assert.Equal(creditCard.ExpiryDate, result.ExpiryDate);
        }

        [Fact]
        public async Task GiveRecordExistsShouldThrowRecordExistingException()
        {
            await ResetDatabase();
            var creditCards = await CreditCardDataFactory.CreateCreditCards(Connection, 1);

            await Assert.ThrowsAsync<CreditCardRecordExistingException>(() =>
                _creditCardRepository.Create(creditCards.First()));
        }
        
        
        [Fact]
        public async Task Give5RecordExistsShouldReturn_WhenCallingGet()
        {
            await ResetDatabase();
            var creditCards = await CreditCardDataFactory.CreateCreditCards(Connection, 5);

            var result = await _creditCardRepository.Get();
            
            foreach (var verifyModel in creditCards.Select(dbModel => result.Where(r =>
                r.CardNumber == dbModel.CardNumber
                && r.Cvc == dbModel.Cvc
                && r.CvcSalt == dbModel.CvcSalt
                && r.Name == dbModel.Name
                && r.ExpiryDate == dbModel.ExpiryDate)))
            {
                Assert.NotNull(verifyModel);
            }
        }
        
        
    }
}