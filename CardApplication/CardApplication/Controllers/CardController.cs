using System.Threading;
using System.Threading.Tasks;
using CardApplication.Application.Handlers;
using CardApplication.Application.Models;
using CardApplication.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CardApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CardController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(CardInput cardInput, CancellationToken cancellationToken)
        {
            try
            {
                var command = new CardRegisterCommand(cardInput);
                await _mediator.Send(command, cancellationToken);
                return NoContent();
            }
            catch (RecordExistingException)
            {
                return Conflict();
            }
        }
    }
}