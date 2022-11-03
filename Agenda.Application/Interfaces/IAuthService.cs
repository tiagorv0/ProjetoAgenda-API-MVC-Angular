using System.Security.Claims;

namespace Agenda.Application.Interfaces
{
    public interface IAuthService
    {
        int Id { get; }
        string Name { get; }
        string Email { get; }
        bool IsAuthenticated();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaims();
    }
}
