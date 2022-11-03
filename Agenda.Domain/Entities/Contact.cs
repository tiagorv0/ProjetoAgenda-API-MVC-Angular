using Agenda.Domain.Core;

namespace Agenda.Domain.Entities
{
    public class Contact : Register
    {
        public string Name { get; set; }
        public IEnumerable<Phone> Phones { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
