using System.Threading;
using System.Threading.Tasks;
using CardApplication.Application.Models;
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
        protected override Task Handle(CardRegisterCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}