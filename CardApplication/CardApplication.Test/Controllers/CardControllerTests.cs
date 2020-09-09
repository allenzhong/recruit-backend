using System.Threading;
using System.Threading.Tasks;
using CardApplication.Application.Handlers;
using CardApplication.Application.Models;
using CardApplication.Controllers;
using CardApplication.Exceptions;
using CardApplication.Test.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CardApplication.Test.Controllers
{
    [Trait("Category", "Unittest")]
    public class CardControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private CardController _controller;
        private CardInput _validInput;

        public CardControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _validInput = CardInputGenerator.CreateValidFaker().Generate();
            _controller = new CardController(_mediatorMock.Object);
        }
        
        [Fact]
        public async Task ShouldCallHandler_WhenModelStateIsValid()
        {
            await _controller.Register(_validInput);

            _mediatorMock.Verify(
                m => m.Send(
                    It.Is<CardRegisterCommand>(
                        c => c.CardInput == _validInput), CancellationToken.None),
                Times.Once);
        }
        
        [Fact]
        public async Task ShouldReturnNoContent_WhenModelStateIsValid()
        {
            var result = await _controller.Register(_validInput);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task ShouldReturnConflict_WhenThrowingRecordExistingException()
        {
            _mediatorMock.Setup(
                    m => m.Send(
                        It.IsAny<CardRegisterCommand>(), default))
                .ThrowsAsync(new RecordExistingException());
            var result = await _controller.Register(_validInput);

            Assert.IsType<ConflictResult>(result);
        }
    }
}