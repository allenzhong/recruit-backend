using System.Threading;
using System.Threading.Tasks;
using CardApplication.Application.Models;
using CardApplication.Domain.Models;
using CardApplication.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CardApplication.Application.Handlers
{
    public class CardRegisterCommand : IRequest
    {
        public CardInput CardInput { get; set; }

        public CardRegisterCommand(CardInput cardInput)
        {
            CardInput = cardInput;
        }
    }
    
    public class CardRegisterCommandHandler : AsyncRequestHandler<CardRegisterCommand>
    {
        private ICreditCardRepository _repository;
        private ILogger<CardRegisterCommandHandler> _logger;

        public CardRegisterCommandHandler(ICreditCardRepository repository, ILogger<CardRegisterCommandHandler> logger)
        {
            _logger = logger;
            _repository = repository;
        }
        protected override async Task Handle(CardRegisterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Start handler");
            var cardInput = request.CardInput;
            var creditCard = new CreditCard()
            {
                Name = cardInput.Name,
                CardNumber = cardInput.CardNumber,
                Cvc = cardInput.Cvc,
                ExpiryDate = cardInput.ExpiryDate
            };
            
            await _repository.Create(creditCard, cancellationToken);
            _logger.LogTrace("Created credit card");
        }
    }
}