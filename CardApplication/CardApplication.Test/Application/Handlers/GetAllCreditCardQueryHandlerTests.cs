using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CardApplication.Application.Handlers;
using CardApplication.Application.Models;
using CardApplication.Infrastructure.Repositories;
using CardApplication.Test.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CardApplication.Test.Application.Handlers
{
    [Trait("Category", "Unittest")]
    public class GetAllCreditCardQueryHandlerTests
    {
        private Mock<ICreditCardRepository> _respositoryMock;
        private GetAllCreditCardQuery _query;
        private IRequestHandler<GetAllCreditCardQuery, IEnumerable<CreditCartOutput>> _handler;
        private readonly Mock<ILogger<GetAllCreditCardQueryHandler>> _logger;
        
        public GetAllCreditCardQueryHandlerTests()
        {
            _logger = new Mock<ILogger<GetAllCreditCardQueryHandler>>(); 
            _respositoryMock = new Mock<ICreditCardRepository>();
            _query = new GetAllCreditCardQuery();
            _handler = new GetAllCreditCardQueryHandler(_respositoryMock.Object, _logger.Object);
        }

        [Fact]
        public void ShouldCallRepositoryGetMethod()
        {
            _handler.Handle(_query, CancellationToken.None);
            
            _respositoryMock.Verify(r => 
                r.Get(), Times.Once);
        }

        [Fact]
        public async Task ShouldReturnListOfOutputModel()
        {
            var dbModels = CreditCardGenerator.CreateValidCreditCardFaker().Generate(2);

            _respositoryMock.Setup(r => r.Get()).ReturnsAsync(dbModels);
            
            var result = (await _handler.Handle(_query, CancellationToken.None)).ToList();

            var expected1 = dbModels[0];
            var actual1 = result[0];
            
            Assert.True(expected1.CardNumber == actual1.CardNumber
                        && expected1.Name == actual1.Name
                        && expected1.ExpiryDate == actual1.ExpiryDate);

            var expected2 = dbModels[1];
            var actual2 = result[1];
            
            Assert.True(expected2.CardNumber == actual2.CardNumber
                        && expected2.Name == actual2.Name
                        && expected2.ExpiryDate == actual2.ExpiryDate);

        }
    }
}