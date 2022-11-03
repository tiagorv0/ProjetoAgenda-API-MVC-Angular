using System.Linq.Expressions;
using Agenda.Application.Exceptions;
using Agenda.Application.Interfaces;
using Agenda.Application.Params;
using Agenda.Application.ViewModels.Contact;
using Agenda.Application.ViewModels.Enums;
using Agenda.Domain.Core;
using Agenda.Domain.Entities;
using Agenda.Domain.Entities.Enums;
using Agenda.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Application.Services
{
    public class AgendaService : IAgendaService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IInteractionRepository _interactionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<RequestContactViewModel> _validator;
        private readonly IAuthService _authService;
        private IRulesValidation _rulesValidation;

        public AgendaService(IContactRepository agendaRepository,
                            IMapper mapper,
                            IInteractionRepository interactionRepository,
                            IUnitOfWork unityOfWork,
                            IValidator<RequestContactViewModel> validator,
                            IAuthService authService, IRulesValidation rulesValidation)
        {
            _contactRepository = agendaRepository;
            _mapper = mapper;
            _interactionRepository = interactionRepository;
            _unitOfWork = unityOfWork;
            _validator = validator;
            _authService = authService;
            _rulesValidation = rulesValidation;
        }

        public async Task<ResponseContactViewModel> CreateAsync(RequestContactViewModel contact)
        {
            _rulesValidation.GetIdsForValidation(_authService.Id);
            var validation = await _validator.ValidateAsync(contact);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            var result = _mapper.Map<Contact>(contact);
            result.UserId = _authService.Id;

            var created = await _contactRepository.CreateAsync(result);

            await _interactionRepository.CreateAsync(new Interaction
                (_authService.Id, InteractionType.CreateContact.Id, InteractionType.CreateContact.Name));
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ResponseContactViewModel>(created);
        }

        public async Task<ResponseContactViewModel> UpdateAsync(RequestContactViewModel contact, int id)
        {
            var contactExist = await _contactRepository.GetByIdAsync(id, x => x.Include(p => p.Phones));
            if (contactExist == null)
                throw new NotFoundException();

            if (contactExist.UserId != _authService.Id)
                throw new NotAuthorizedException();

            _rulesValidation.GetIdsForValidation(_authService.Id, id);
            var validation = await _validator.ValidateAsync(contact);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            _mapper.Map<RequestContactViewModel, Contact>(contact, contactExist);

            var updated = await _contactRepository.UpdateAsync(contactExist);

            await _interactionRepository.CreateAsync(new Interaction
                (_authService.Id, InteractionType.UpdateContact.Id, InteractionType.UpdateContact.Name));
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ResponseContactViewModel>(updated);
        }

        public async Task RemoveAsync(int id)
        {
            var contactExist = await _contactRepository.GetByIdAsync(id);
            if (contactExist == null)
                throw new NotFoundException();

            if (contactExist.UserId != _authService.Id)
                throw new NotAuthorizedException();

            await _contactRepository.DeleteAsync(id);

            await _interactionRepository.CreateAsync(new Interaction
                (_authService.Id, InteractionType.RemoveContact.Id, InteractionType.RemoveContact.Name));
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ResponseContactViewModel>> GetAllAsync(Expression<Func<Contact, bool>>? expression = null, int? skip = null, int? take = null)
        {
            await _interactionRepository.CreateAsync(new Interaction
                (_authService.Id, InteractionType.GetContact.Id, InteractionType.GetContact.Name));

            var obj = await _contactRepository.GetAllAsync(x => x.UserId == _authService.Id, x => x.Include(p => p.Phones).ThenInclude(pt => pt.PhoneType), skip: skip, take: take);
            return _mapper.Map<IEnumerable<ResponseContactViewModel>>(obj);
        }

        public async Task<ResponseContactViewModel> GetByIdAsync(int id)
        {
            var contactExist = await _contactRepository.GetByIdAsync(id, x => x.Include(p => p.Phones).ThenInclude(pt => pt.PhoneType));
            if (contactExist == null)
                throw new NotFoundException();

            await _interactionRepository.CreateAsync(new Interaction
                (_authService.Id, InteractionType.GetContact.Id, InteractionType.GetContact.Name));

            return _mapper.Map<ResponseContactViewModel>(contactExist);
        }

        public async Task<IEnumerable<ResponseContactViewModel>> GetParamsAsync(ContactParams contactParams, int? skip = null, int? take = null)
        {
            await _interactionRepository.CreateAsync(new Interaction
                (_authService.Id, InteractionType.GetContact.Id, InteractionType.GetContact.Name));

            var result = await _contactRepository.GetAllAsync(contactParams.Filter(), x => x.Include(p => p.Phones).ThenInclude(pt => pt.PhoneType), skip: skip, take: take);

            return _mapper.Map<IEnumerable<ResponseContactViewModel>>(result.Where(x => x.UserId == _authService.Id));
        }

        public async Task<int> CountAsync(ContactParams contactParams)
        {
            var result = await GetParamsAsync(contactParams);
            return result.Count();
        }

        public async Task<bool> IdExists(int id)
        {
            return (await _contactRepository.GetByIdAsync(id)) != null;
        }

        public IEnumerable<PhoneTypeViewModel> GetPhoneTypes()
        {
            return _mapper.Map<IEnumerable<PhoneTypeViewModel>>(Enumeration.GetAll<PhoneType>());
        }
    }
}
