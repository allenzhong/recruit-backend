using System.Threading;
using System.Threading.Tasks;
using CardApplication.Application.Handlers;
using CardApplication.Application.Models;
using CardApplication.Controllers;
using CardApplication.Test.Application.Models;
using MediatR;
using Moq;
using Xunit;

namespace CardApplication.Test.Controllers
{
    [Trait("Category", "Unittest")]
    public class CardControllerTests
    {
        [Fact]
        public async Task ShouldCallHandlerWhenModelStateIsValid()
        {
            var mediatorMock = new Mock<IMediator>();
            var validInput = CardInputGenerator.CreateValidFaker().Generate();

            var controller = new CardController(mediatorMock.Object);

            await controller.Register(validInput);

            mediatorMock.Verify(
                m => m.Send(
                    It.Is<CardRegisterCommand>(
                        c => c.CardInput == validInput), CancellationToken.None),
                Times.Once);
        }
    }
}