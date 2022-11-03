using Agenda.Application.Interfaces;
using Agenda.Application.ViewModels.Contact;
using Microsoft.AspNetCore.Mvc;
using Agenda.Application.Params;
using Microsoft.AspNetCore.Authorization;
using Agenda.Application.ViewModels;

namespace Agenda.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaService _agendaService;

        public AgendaController(IAgendaService agendaService)
        {
            _agendaService = agendaService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RequestContactViewModel model)
        {
            var obj = await _agendaService.CreateAsync(model);
            return Ok(obj);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAsync([FromBody] RequestContactViewModel model, [FromRoute] int id)
        {
            var obj = await _agendaService.UpdateAsync(model, id);

            return Ok(obj);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await _agendaService.RemoveAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int? skip = null, int? take = null)
        {
            var contacts = await _agendaService.GetAllAsync(skip: skip, take: take);
            return Ok(contacts);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var contact = await _agendaService.GetByIdAsync(id);

            return Ok(contact);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetParamsAsync([FromQuery] ContactParams contactParams)
        {
            var obj = new PaginationResponse<ResponseContactViewModel>
            {
                Data = await _agendaService.GetParamsAsync(contactParams, contactParams.Skip, contactParams.Take),
                Total = await _agendaService.CountAsync(contactParams),
                Take = contactParams.Take,
                Skip = contactParams.Skip,
            };
            return Ok(obj);
        }

        [HttpGet("phone-types")]
        public IActionResult GetPhoneTypes()
        {
            var obj = _agendaService.GetPhoneTypes();
            return Ok(obj);
        }
    }
}
