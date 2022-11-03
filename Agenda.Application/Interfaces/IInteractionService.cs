using Agenda.Application.ViewModels.Enums;
using Agenda.Application.ViewModels.Interaction;

namespace Agenda.Application.Interfaces
{
    public interface IInteractionService
    {
        Task<IEnumerable<ResponseInteractionViewModel>> GetAllAsync();
        IEnumerable<InteractionTypeViewModel> GetInteractionType();
        Task SaveJsonInteractionAsync();
    }
}
