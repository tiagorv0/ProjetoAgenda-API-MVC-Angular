using Agenda.Application.Constants;
using Agenda.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Api.Controllers
{
    [Route("api/v1/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class LogController : ControllerBase
    {
        private readonly IInteractionService _interactionService;

        public LogController(IInteractionService interactionService)
        {
            _interactionService = interactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var interactions = await _interactionService.GetAllAsync();
            return Ok(interactions);
        }

        [HttpGet("types")]
        public IActionResult GetInteractionType()
        {
            var interactionsTypes = _interactionService.GetInteractionType();
            return Ok(interactionsTypes);
        }

        [HttpGet("save-json")]
        public async Task<IActionResult> SaveJsonAsync()
        {
            await _interactionService.SaveJsonInteractionAsync();
            return Ok();
        }
    }
}
