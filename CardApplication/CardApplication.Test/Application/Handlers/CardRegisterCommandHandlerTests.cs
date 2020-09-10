using System.Threading;
using CardApplication.Application.Handlers;
using CardApplication.Application.Models;
using CardApplication.Domain.Models;
using CardApplication.Infrastructure.Repositories;
using CardApplication.Test.Application.Models;
using MediatR;
using Moq;
using Xunit;

namespace CardApplication.Test.Application.Handlers
{
    public class CardRegisterCommandHandlerTests
    {
        private Mock<ICreditCardRepository> _respositoryMock;
        private CardInput _cardInput;
        private CardRegisterCommand _command;
        private IRequestHandler<CardRegisterCommand> _handler;

        public CardRegisterCommandHandlerTests()
        {
            _respositoryMock = new Mock<ICreditCardRepository>();
            _cardInput = CardInputGenerator.CreateValidFaker().Generate();
            _command = new CardRegisterCommand(_cardInput);
            _handler = new CardRegisterCommandHandler(_respositoryMock.Object);
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