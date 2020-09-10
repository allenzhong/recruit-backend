using System.Threading;
using System.Threading.Tasks;
using CardApplication.Application.Models;
using CardApplication.Domain.Models;
using CardApplication.Infrastructure.Repositories;
using MediatR;

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

        public CardRegisterCommandHandler(ICreditCardRepository repository)
        {
            _repository = repository;
        }
        protected override async Task Handle(CardRegisterCommand request, CancellationToken cancellationToken)
        {
            var cardInput = request.CardInput;
            var creditCard = new CreditCard()
            {
                Id = request.CardInput.Id, 
                Name = cardInput.Name,
                CardNumber = cardInput.CardNumber,
                Cvc = cardInput.Cvc,
                ExpiryDate = cardInput.ExpiryDate
            };
            
            await _repository.Create(creditCard, cancellationToken);
        }
    }
}