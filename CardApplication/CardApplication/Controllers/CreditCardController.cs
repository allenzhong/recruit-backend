using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CardApplication.Application.Handlers;
using CardApplication.Application.Models;
using CardApplication.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CardApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditCardController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public CreditCardController(IMediator mediator, ILogger<CreditCardController> logger)
        {
            _logger = logger;
            _mediator = mediator;
            
            _logger.LogDebug("CardController Constructor");
        }
        
        [HttpPost]
        [Route("register")]
        [Authorize]
        public async Task<IActionResult> Register(CardInput cardInput, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Begin: Register");
            try
            {
                var command = new CardRegisterCommand(cardInput);
                await _mediator.Send(command, cancellationToken);
                return NoContent();
            }
            catch (CreditCardRecordExistingException)
            {
                _logger.LogTrace("Throw RecordExistingException");
                return Conflict();
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            _logger.LogTrace("Begin: Get All");
 
            var query = new GetAllCreditCardQuery();
            var records = await _mediator.Send(query);
 
            return Ok(records);
        }
        
    }
}