using System.Linq.Expressions;
using Agenda.Application.Exceptions;
using Agenda.Application.Interfaces;
using Agenda.Application.Params;
using Agenda.Application.ViewModels.AdminContact;
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
    public class AgendaAdminService : IAgendaAdminService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IInteractionRepository _interactionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<RequestAdminContactViewModel> _validator;
        private readonly IAuthService _authService;
        private readonly IRulesValidation _rulesValidation;

        public AgendaAdminService(IContactRepository contactRepository,
                                    IInteractionRepository interactionRepository,
                                    IUnitOfWork unitOfWork, IMapper mapper,
                                    IValidator<RequestAdminContactViewModel> validator,
                                    IAuthService authService,
                                    IRulesValidation rulesValidation)
        {
            _contactRepository = contactRepository;
            _interactionRepository = interactionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _authService = authService;
            _rulesValidation = rulesValidation;
        }

        public async Task<ResponseAdminContactViewModel> CreateAsync(RequestAdminContactViewModel entity)
        {
            _rulesValidation.GetIdsForValidation(entity.UserId);
            var validation = await _validator.ValidateAsync(entity);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            var result = _mapper.Map<Contact>(entity);
            var created = await _contactRepository.CreateAsync(result);

            await _interactionRepository.CreateAsync(new Interaction
                (_authService.Id, InteractionType.CreateContact.Id, InteractionType.CreateContact.Name));
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ResponseAdminContactViewModel>(created);
        }

        public async Task<ResponseAdminContactViewModel> UpdateAsync(RequestAdminContactViewModel entity, int id)
        {
            var contactExist = await _contactRepository.GetByIdAsync(id, x => x.Include(p => p.Phones));
            if (contactExist == null)
                throw new NotFoundException();

            _rulesValidation.GetIdsForValidation(entity.UserId, id);
            var validation = await _validator.ValidateAsync(entity);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            _mapper.Map<RequestAdminContactViewModel, Contact>(entity, contactExist);

            var updated = await _contactRepository.UpdateAsync(contactExist);

            await _interactionRepository.CreateAsync(new Interaction
                (_authService.Id, InteractionType.UpdateContact.Id, InteractionType.UpdateContact.Name));
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ResponseAdminContactViewModel>(updated);
        }

        public async Task RemoveAsync(int id)
        {
            var contactExist = await _contactRepository.GetByIdAsync(id);
            if (contactExist == null)
                throw new NotFoundException();

            await _contactRepository.DeleteAsync(id);

            await _interactionRepository.CreateAsync(new Interaction
                (_authService.Id, InteractionType.RemoveContact.Id, InteractionType.RemoveContact.Name));
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ResponseAdminContactViewModel>> GetAllAsync(Expression<Func<Contact, bool>> expression = null, int? skip = null, int? take = null)
        {
            await _interactionRepository.CreateAsync(new Interaction
                (_authService.Id, InteractionType.GetContact.Id, InteractionType.GetContact.Name));

            var obj = await _contactRepository.GetAllAsync(expression, x => x.Include(u => u.User).Include(p => p.Phones).ThenInclude(pt => pt.PhoneType), skip: skip, take: take);
            return _mapper.Map<IEnumerable<ResponseAdminContactViewModel>>(obj);
        }

        public async Task<ResponseAdminContactViewModel> GetByIdAsync(int id)
        {
            var contactExist = await _contactRepository.GetByIdAsync(id, x => x.Include(u => u.User).Include(p => p.Phones).ThenInclude(pt => pt.PhoneType));
            if (contactExist == null)
                throw new NotFoundException();

            await _interactionRepository.CreateAsync(new Interaction
                (_authService.Id, InteractionType.GetContact.Id, InteractionType.GetContact.Name));

            return _mapper.Map<ResponseAdminContactViewModel>(contactExist);
        }

        public async Task<IEnumerable<ResponseAdminContactViewModel>> GetParamsAsync(AdminContactParams contactParams, int? skip = null, int? take = null)
        {
            await _interactionRepository.CreateAsync(new Interaction
                (_authService.Id, InteractionType.GetContact.Id, InteractionType.GetContact.Name));

            return _mapper.Map<IEnumerable<ResponseAdminContactViewModel>>(
                await _contactRepository.GetAllAsync(contactParams.Filter(), x => x.Include(u => u.User).Include(p => p.Phones).ThenInclude(pt => pt.PhoneType), skip: skip, take: take));
        }

        public async Task<int> CountAsync(AdminContactParams contactParams)
        {
            return await _contactRepository.CountAsync(contactParams.Filter());
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
