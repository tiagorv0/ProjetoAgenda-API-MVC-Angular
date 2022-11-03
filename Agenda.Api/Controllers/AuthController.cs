using Agenda.Application.Interfaces;
using Agenda.Application.Token;
using Agenda.Application.ViewModels.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ILoginService _loginService;

        public AuthController(ITokenGenerator tokenGenerator, ILoginService loginService)
        {
            _tokenGenerator = tokenGenerator;
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            var obj = await _loginService.Login(loginViewModel);

            return Ok(new 
            {
                token = _tokenGenerator.GenerateToken(obj)
            });
        }
    }
}
