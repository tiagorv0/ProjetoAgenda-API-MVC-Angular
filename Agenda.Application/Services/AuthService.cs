using System.Security.Claims;
using Agenda.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Agenda.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _accessor;

        public AuthService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public int Id => int.Parse(GetClaims().FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
        public string Email => GetClaims().FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        public string Name => GetClaims().FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

        public IEnumerable<Claim> GetClaims()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool IsInRole(string role)
        {
            return _accessor.HttpContext.User.IsInRole(role);
        }
    }
}
