using Agenda.Application.Interfaces;
using Agenda.Application.ViewModels.Enums;
using Agenda.Application.ViewModels.Interaction;
using Agenda.Domain.Core;
using Agenda.Domain.Entities.Enums;
using Agenda.Domain.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Application.Services
{
    public class InteractionService : IInteractionService
    {
        private readonly IInteractionRepository _interactionRepository;
        private readonly IMapper _mapper;

        public InteractionService(IInteractionRepository interactionRepository, IMapper mapper)
        {
            _interactionRepository = interactionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ResponseInteractionViewModel>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<ResponseInteractionViewModel>>(
                await _interactionRepository.GetAllAsync());
        }

        public IEnumerable<InteractionTypeViewModel> GetInteractionType()
        {
            return _mapper.Map<IEnumerable<InteractionTypeViewModel>>(
                Enumeration.GetAll<InteractionType>());
        }

        public async Task SaveJsonInteractionAsync()
        {
            await _interactionRepository.SaveJsonInteractionsAsync();
        }
    }
}
