using System.Threading;
using System.Threading.Tasks;
using CardApplication.Application.Handlers;
using CardApplication.Application.Models;
using CardApplication.Domain.Models;
using CardApplication.Exceptions;
using CardApplication.Infrastructure.Repositories;
using CardApplication.Test.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CardApplication.Test.Application.Handlers
{
    [Trait("Category", "Unittest")]
    public class GetCreditCardByIdQueryHandlerTests
    {
        private Mock<ICreditCardRepository> _respositoryMock;
        private IRequestHandler<GetCreditCardByIdQuery, CreditCartOutput> _handler;
        private readonly Mock<ILogger<GetCreditCardByIdQueryHandler>> _logger;
        
        public GetCreditCardByIdQueryHandlerTests()
        {
            _logger = new Mock<ILogger<GetCreditCardByIdQueryHandler>>(); 
            _respositoryMock = new Mock<ICreditCardRepository>();
            _handler = new GetCreditCardByIdQueryHandler(_respositoryMock.Object, _logger.Object);
        }
        
        [Fact]
        public void ShouldCallRepositoryGetByIdMethod()
        {
            var id = 123L;
            var query = new GetCreditCardByIdQuery(id);
            _handler.Handle(query, CancellationToken.None);
            
            _respositoryMock.Verify(r => 
                r.GetById(It.Is<long>(l => l == id)), Times.Once);
        }
        
        [Fact]
        public async Task ShouldReturnSingleModel()
        {
            var dbModel = CreditCardGenerator.CreateValidCreditCardFaker()
                .RuleFor(r => r.Id, f => f.Random.Long())
                .Generate();

            _respositoryMock.Setup(r => r.GetById(It.IsAny<long>())).ReturnsAsync(dbModel);
            
            var query = new GetCreditCardByIdQuery(dbModel.Id);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(dbModel.Id, result.Id);
            Assert.Equal(dbModel.Name, result.Name);
            Assert.Equal(dbModel.CardNumber, result.CardNumber);
            Assert.Equal(dbModel.ExpiryDate, result.ExpiryDate);
        }
        
        [Fact]
        public async Task ShouldThrowRecordNotFoundException_WhenGetByIdReturnsNull()
        {
            _respositoryMock.Setup(r => r.GetById(It.IsAny<long>())).ReturnsAsync((CreditCard) null);
            
            var query = new GetCreditCardByIdQuery(123L);

            await Assert.ThrowsAsync<RecordNotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}