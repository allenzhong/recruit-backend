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
    public class CardController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public CardController(IMediator mediator, ILogger<CardController> logger)
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
            catch (RecordExistingException)
            {
                _logger.LogTrace("Throw RecordExistingException");
                return Conflict();
            }
        }
    }
}