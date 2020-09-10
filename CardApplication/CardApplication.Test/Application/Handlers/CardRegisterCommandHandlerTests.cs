using System.Threading;
using CardApplication.Application.Handlers;
using CardApplication.Application.Models;
using CardApplication.Domain.Models;
using CardApplication.Infrastructure.Repositories;
using CardApplication.Test.Application.Models;
using CardApplication.Test.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CardApplication.Test.Application.Handlers
{
    [Trait("Category", "Unittest")]
    public class CardRegisterCommandHandlerTests
    {
        private Mock<ICreditCardRepository> _respositoryMock;
        private CardInput _cardInput;
        private CardRegisterCommand _command;
        private IRequestHandler<CardRegisterCommand> _handler;
        private readonly Mock<ILogger<CardRegisterCommandHandler>> _logger;
        
        public CardRegisterCommandHandlerTests()
        {
            _logger = new Mock<ILogger<CardRegisterCommandHandler>>(); 
            _respositoryMock = new Mock<ICreditCardRepository>();
            _cardInput = CreditCardGenerator.CreateValidCardInputFaker().Generate();
            _command = new CardRegisterCommand(_cardInput);
            _handler = new CardRegisterCommandHandler(_respositoryMock.Object, _logger.Object);
        }

        [Fact]
        public void ShouldCallRepositoryCreate()
        {
            _handler.Handle(_command, CancellationToken.None);

            _respositoryMock.Verify(
                r => 
                    r.Create(It.Is<CreditCard>(c =>
                           c.Id == _cardInput.Id
                        && c.Name == _cardInput.Name
                        && c.CardNumber == _cardInput.CardNumber
                        && c.Cvc == _cardInput.Cvc
                        && c.ExpiryDate == _cardInput.ExpiryDate), CancellationToken.None),
                        Times.Once());
        }   
    }
}