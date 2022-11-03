using Agenda.Domain.Core;
using Agenda.Domain.Entities.Enums;

namespace Agenda.Domain.Entities
{
    public class Interaction : Register
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int InteractionTypeId { get; set; }
        public InteractionType InteractionType { get; set; }
        public string Message { get; set; }

        public Interaction(int userId, int interactionTypeId , string message)
        {
            UserId = userId;
            InteractionTypeId = interactionTypeId;
            Message = message;
        }
    }
}
