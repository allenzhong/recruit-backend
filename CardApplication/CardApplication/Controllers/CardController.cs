using System.Threading.Tasks;
using CardApplication.Application.Handlers;
using CardApplication.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CardApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        private IMediator _mediator;

        public CardController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(CardInput cardInput)
        {
            if (!ModelState.IsValid) return null;
            var command = new CardRegisterCommand(cardInput);
            await _mediator.Send(command);
            return null;
        }
    }
}