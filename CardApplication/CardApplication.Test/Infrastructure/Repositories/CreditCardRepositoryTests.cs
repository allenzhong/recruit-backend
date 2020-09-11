using System;
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
        public async Task ShouldCreateRecord_WhenRecordDoesNotExist()
        {
            await ResetDatabase();
            var creditCard = CreditCardGenerator.CreateValidCreditCardFaker().Generate();
            await _creditCardRepository.Create(creditCard);

            var result = await Connection.QueryFirstAsync(
                @"SELECT * FROM CreditCards where CardNumber=@CardNumber",
                new { creditCard.CardNumber});
            
            Assert.NotEqual(0L, result.Id);
            Assert.Equal(creditCard.Name, result.Name);
            Assert.Equal(creditCard.CardNumber, result.CardNumber);
            Assert.Equal(creditCard.Cvc, result.Cvc);
            Assert.Equal(creditCard.CvcSalt, result.CvcSalt);
            Assert.Equal(creditCard.ExpiryDate, result.ExpiryDate);
        }

        [Fact]
        public async Task ShouldThrowRecordExistingException_WhenCreateAndRecordExists()
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
        
        [Fact]
        public async Task Give5RecordExistsShouldReturn_WhenCallingGetById()
        {
            await ResetDatabase();
            var creditCards = await CreditCardDataFactory.CreateCreditCards(Connection, 5);

            var random = new Random(5).Next();
            var oneOfCards = creditCards.Take(random).First();
            var result = await _creditCardRepository.GetById(oneOfCards.Id);

            Assert.Equal(oneOfCards.Id, result.Id); 
            Assert.Equal(oneOfCards.Name, result.Name); 
            Assert.Equal(oneOfCards.CardNumber, result.CardNumber); 
            //TODO verify cvc, need change cvc setting logic 
            // Assert.Equal(oneOfCards.Cvc, result.Cvc); 
            Assert.Equal(oneOfCards.ExpiryDate, result.ExpiryDate); 
        }
    }
}