using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CardApplication.Application.Handlers;
using CardApplication.Application.Models;
using CardApplication.Controllers;
using CardApplication.Exceptions;
using CardApplication.Test.Application.Models;
using CardApplication.Test.Helpers;
using Castle.Core.Internal;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CardApplication.Test.Controllers
{
    [Trait("Category", "Unittest")]
    public class CardControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CreditCardController _controller;
        private readonly CardInput _validInput;
        private readonly Mock<ILogger<CreditCardController>> _logger;

        public CardControllerTests()
        {
            _logger = new Mock<ILogger<CreditCardController>>();
            _mediatorMock = new Mock<IMediator>();
            _validInput = CreditCardGenerator.CreateValidCardInputFaker().Generate();
            _controller = new CreditCardController(_mediatorMock.Object, _logger.Object);
        }
        
        [Fact]
        public async Task ShouldCallHandler_WhenModelStateIsValid()
        {
            await _controller.Register(_validInput, CancellationToken.None);

            _mediatorMock.Verify(
                m => m.Send(
                    It.Is<CardRegisterCommand>(
                        c => c.CardInput == _validInput), CancellationToken.None),
                Times.Once);
        }
        
        [Fact]
        public async Task ShouldReturnNoContent_WhenModelStateIsValid()
        {
            var result = await _controller.Register(_validInput, CancellationToken.None);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task ShouldReturnConflict_WhenThrowingRecordExistingException()
        {
            _mediatorMock.Setup(
                    m => m.Send(
                        It.IsAny<CardRegisterCommand>(), default))
                .ThrowsAsync(new CreditCardRecordExistingException());
            var result = await _controller.Register(_validInput, CancellationToken.None);

            Assert.IsType<ConflictResult>(result);
        }
        
        [Fact]
        public void ShouldHaveRequiredAttributes_OnRegister()
        {
            var controllerType = _controller.GetType();
            var methodInfo = controllerType.GetMethod("Register");

            Assert.NotNull(methodInfo.GetAttribute<HttpPostAttribute>());
        }
        
        [Theory]
        [InlineData("Register", null, "register")]
        public void ShouldHaveRouteAttributes_OnMethods(string methodName, Type[] types, string expectedTemplate)
        {
            AssertRouteTemplate<CreditCardController>(methodName, types, expectedTemplate);
        }
        
        private void AssertRouteTemplate<T>(string methodName, Type[] methodParameterTypes,
            string expectedTemplate)
        {
            var controllerType = typeof(T);
            var method = methodParameterTypes != null && methodParameterTypes.Any() ?
                controllerType.GetMethod(methodName, methodParameterTypes)
                : controllerType.GetMethod(methodName);
            var routeAttribute = method.GetAttribute<RouteAttribute>();

            Assert.True(routeAttribute.Template == expectedTemplate);
        }
    }
}