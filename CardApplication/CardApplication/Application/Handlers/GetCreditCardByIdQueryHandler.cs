using System.Threading;
using System.Threading.Tasks;
using CardApplication.Application.Models;
using CardApplication.Exceptions;
using CardApplication.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

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
        private ICreditCardRepository _creditCardRepository;
        private ILogger<GetCreditCardByIdQueryHandler> _logger;

        public GetCreditCardByIdQueryHandler(ICreditCardRepository creditCardRepository, ILogger<GetCreditCardByIdQueryHandler> logger)
        {
            _logger = logger;
            _creditCardRepository = creditCardRepository;
        }

        public async Task<CreditCartOutput> Handle(GetCreditCardByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Handler Begin");
            
            var dbModel = await _creditCardRepository.GetById(request.Id);
            
            if(dbModel == null) throw new RecordNotFoundException();
            
            var result = new CreditCartOutput
            {
                Id = dbModel.Id,
                CardNumber = dbModel.CardNumber,
                Name = dbModel.Name,
                ExpiryDate = dbModel.ExpiryDate
            };

            _logger.LogTrace("Return Record");
            return result;
        }
    }
}