using Agenda.Application.Constants;
using Agenda.Application.Interfaces;
using Agenda.Application.Params;
using Agenda.Application.ViewModels;
using Agenda.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Api.Controllers
{
    [Route("api/v1/admin/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> PostAsync([FromBody] RequestUserViewModel model)
        {
            var obj = await _userService.CreateAsync(model);
            return Ok(obj);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAsync([FromBody] RequestUserViewModel model, [FromRoute] int id)
        {
            var obj = await _userService.UpdateAsync(model, id);

            return Ok(obj);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await _userService.RemoveAsync(id);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            return Ok(user);
        }

        [HttpGet("search")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetParamsAsync([FromQuery] UserParams userParams)
        {
            var obj = new PaginationResponse<ResponseUserViewModel>
            {
                Data = await _userService.GetParamsAsync(userParams, userParams.Skip, userParams.Take),
                Total = await _userService.CountAsync(userParams),
                Take = userParams.Take,
                Skip = userParams.Skip,
            };
            return Ok(obj);
        }

        [HttpGet("user-roles")]
        public IActionResult GetUserRoles()
        {
            var obj = _userService.GetUserRole();
            return Ok(obj);
        }

        [HttpGet("get-own-user")]
        public async Task<IActionResult> GetOwnUserAsync()
        {
            var user = await _userService.GetOwnUserAsync();
            return Ok(user);
        }
    }
}
