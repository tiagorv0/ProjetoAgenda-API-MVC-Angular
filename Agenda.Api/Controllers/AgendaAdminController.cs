using Agenda.Application.Constants;
using Agenda.Application.Interfaces;
using Agenda.Application.Params;
using Agenda.Application.ViewModels;
using Agenda.Application.ViewModels.AdminContact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Api.Controllers
{
    [Route("api/v1/admin/agenda")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class AgendaAdminController : ControllerBase
    {
        private readonly IAgendaAdminService _agendaAdminService;

        public AgendaAdminController(IAgendaAdminService agendaAdminService)
        {
            _agendaAdminService = agendaAdminService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RequestAdminContactViewModel model)
        {
            var obj = await _agendaAdminService.CreateAsync(model);
            return Ok(obj);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAsync([FromBody] RequestAdminContactViewModel model, [FromRoute] int id)
        {
            var obj = await _agendaAdminService.UpdateAsync(model, id);

            return Ok(obj);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await _agendaAdminService.RemoveAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int? skip = null, int? take = null)
        {
            var contacts = await _agendaAdminService.GetAllAsync(skip: skip, take: take);
            return Ok(contacts);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var contact = await _agendaAdminService.GetByIdAsync(id);

            return Ok(contact);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetParamsAsync([FromQuery] AdminContactParams contactParams)
        {
            var obj = new PaginationResponse<ResponseAdminContactViewModel>
            {
                Data = await _agendaAdminService.GetParamsAsync(contactParams, contactParams.Skip, contactParams.Take),
                Total = await _agendaAdminService.CountAsync(contactParams),
                Take = contactParams.Take,
                Skip = contactParams.Skip,
            };
            return Ok(obj);
        }

        [HttpGet("phone-types")]
        public IActionResult GetPhoneTypes()
        {
            var obj = _agendaAdminService.GetPhoneTypes();
            return Ok(obj);
        }
    }
}
