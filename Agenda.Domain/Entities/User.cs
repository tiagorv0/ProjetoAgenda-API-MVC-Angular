using Agenda.Domain.Core;
using Agenda.Domain.Entities.Enums;

namespace Agenda.Domain.Entities
{
    public class User : Register
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
        public int UserRoleId { get; set; }
    }
}
