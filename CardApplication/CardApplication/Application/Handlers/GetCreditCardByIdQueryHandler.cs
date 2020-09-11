using System.Threading;
using System.Threading.Tasks;
using CardApplication.Application.Models;
using MediatR;

namespace CardApplication.Application.Handlers
{
    public class GetCreditCardByIdQuery : IRequest<CreditCartOutput>
    {
        public GetCreditCardByIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }

    public class GetCreditCardByIdQueryHandler : IRequestHandler<GetCreditCardByIdQuery, CreditCartOutput>
    {
        public Task<CreditCartOutput> Handle(GetCreditCardByIdQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}