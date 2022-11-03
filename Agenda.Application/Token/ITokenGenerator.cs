using System.Security.Claims;

namespace Agenda.Application.Token
{
    public interface ITokenGenerator
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}
