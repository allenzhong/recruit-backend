using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CardApplication.Application.Models;
using CardApplication.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CardApplication.Application.Handlers
{
    public class GetAllCreditCardQuery : IRequest<IEnumerable<CreditCartOutput>>
    {
        
    }
    
    public class GetAllCreditCardQueryHandler : IRequestHandler<GetAllCreditCardQuery, IEnumerable<CreditCartOutput>>
    {
        private ICreditCardRepository _creditCardRepository;
        private ILogger<GetAllCreditCardQueryHandler> _logger;

        public GetAllCreditCardQueryHandler(ICreditCardRepository creditCardRepository, ILogger<GetAllCreditCardQueryHandler> logger)
        {
            _logger = logger;
            _creditCardRepository = creditCardRepository;
        }

        public async Task<IEnumerable<CreditCartOutput>> Handle(GetAllCreditCardQuery request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Handle Begin");
            var dbResult = await _creditCardRepository.Get();
            
            var result = dbResult.Select(r => new CreditCartOutput()
            {
                Id = r.Id,
                Name = r.Name,
                CardNumber = r.CardNumber,
                ExpiryDate = r.ExpiryDate
            });

            _logger.LogTrace("Handle Return");
            return result;
        }
    }
}