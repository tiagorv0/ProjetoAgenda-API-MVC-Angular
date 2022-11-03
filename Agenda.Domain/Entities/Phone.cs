using Agenda.Domain.Core;
using Agenda.Domain.Entities.Enums;

namespace Agenda.Domain.Entities
{
    public class Phone : Register
    {
        public string Description { get; set; }
        public string FormattedPhone { get; set; }
        public int DDD { get; set; }
        public string Number { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
        public int PhoneTypeId { get; set; }
        public PhoneType PhoneType { get; set; }
    }
}
