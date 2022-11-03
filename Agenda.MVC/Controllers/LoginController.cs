using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Agenda.MVC.Data;
using Agenda.MVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IApiLoginService _apiLoginService;
        public LoginController(IApiLoginService apiLoginService)
        {
            _apiLoginService = apiLoginService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            var result = await _apiLoginService.Login(loginViewModel);

            if (!result.IsSuccessStatusCode)
            {
                TempData["error"] = "Falha ao realizar login";
                return View(loginViewModel);
            }

            await LoginAuthenticate(result.Content.Token);

            return RedirectToAction("Index", "Agenda");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("Index");
        }

        private async Task LoginAuthenticate(string token)
        {
            var claimsIdentity = new ClaimsIdentity(DecodeToken(token),
                CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

             await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
        }

        private IEnumerable<Claim> DecodeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);
            var list = new List<Claim>() { new Claim("Bearer", token) };
            list.AddRange(decodedToken.Claims);
            return list;
        }
    }
}
