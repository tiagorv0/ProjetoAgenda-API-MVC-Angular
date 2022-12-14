using Agenda.Domain.Core;

namespace Agenda.Application.ViewModels.Interaction
{
    public class ResponseInteractionViewModel : Register
    {
        public int UserId { get; set; }
        public int InteractionTypeId { get; set; }
        public string Message { get; set; }
    }
}
