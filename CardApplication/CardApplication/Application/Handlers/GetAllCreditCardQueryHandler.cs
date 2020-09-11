using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CardApplication.Application.Models;
using MediatR;

namespace CardApplication.Application.Handlers
{
    public class GetAllCreditCardQuery : IRequest<IEnumerable<CreditCartOutput>>
    {
        
    }
    
    public class GetAllCreditCardQueryHandler : IRequestHandler<GetAllCreditCardQuery, IEnumerable<CreditCartOutput>>
    {
        public Task<IEnumerable<CreditCartOutput>> Handle(GetAllCreditCardQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}