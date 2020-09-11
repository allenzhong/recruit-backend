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
            var creditCard = new CreditCard();
            creditCard.Name = cardInput.Name;
            creditCard.CardNumber = cardInput.CardNumber;
            creditCard.CvcSalt = CreditCard.GenerateSalt(5);
            creditCard.Cvc = cardInput.Cvc;
            creditCard.ExpiryDate = cardInput.ExpiryDate;
            
            await _repository.Create(creditCard);
            _logger.LogTrace("Created credit card");
        }
    }
}