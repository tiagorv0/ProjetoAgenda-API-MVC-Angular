using System.Linq.Expressions;
using Agenda.Application.Exceptions;
using Agenda.Application.Interfaces;
using Agenda.Application.Params;
using Agenda.Application.ViewModels.Enums;
using Agenda.Application.ViewModels.User;
using Agenda.Domain.Core;
using Agenda.Domain.Entities;
using Agenda.Domain.Entities.Enums;
using Agenda.Domain.Interfaces;
using Agenda.Infrastructure.Utils;
using AutoMapper;
using FluentValidation;

namespace Agenda.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<RequestUserViewModel> _validator;
        private readonly IRulesValidation _rulesValidation;
        private readonly IAuthService _authService;

        public UserService(IUserRepository userRepository,
                            IMapper mapper,
                            IUnitOfWork unitOfWork,
                            IValidator<RequestUserViewModel> validator,
                            IRulesValidation rulesValidation,
                            IAuthService authService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _rulesValidation = rulesValidation;
            _authService = authService;
        }

        public async Task<ResponseUserViewModel> CreateAsync(RequestUserViewModel entity)
        {
            var validation = await _validator.ValidateAsync(entity);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            var result = _mapper.Map<User>(entity);
            PasswordHasher.PasswordHash(result);

            var created = await _userRepository.CreateAsync(result);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ResponseUserViewModel>(created);
        }

        public async Task<ResponseUserViewModel> UpdateAsync(RequestUserViewModel entity, int id)
        {
            var userExist = await _userRepository.GetByIdAsync(id);
            if (userExist == null)
                throw new NotFoundException();

            _rulesValidation.GetIdsForValidation(id);
            var validation = await _validator.ValidateAsync(entity);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            _mapper.Map<RequestUserViewModel, User>(entity, userExist);
            PasswordHasher.PasswordHash(userExist);

            var updated = await _userRepository.UpdateAsync(userExist);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ResponseUserViewModel>(updated);
        }

        public async Task RemoveAsync(int id)
        {
            var userExist = await _userRepository.GetByIdAsync(id);
            if (userExist == null)
                throw new NotFoundException();

            await _userRepository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ResponseUserViewModel>> GetAllAsync(Expression<Func<User, bool>> expression = null, int? skip = null, int? take = null)
        {
            return _mapper.Map<IEnumerable<ResponseUserViewModel>>(await _userRepository.GetAllAsync(expression, skip: skip, take: take));
        }

        public async Task<ResponseUserViewModel> GetByIdAsync(int id)
        {
            var userExist = await _userRepository.GetByIdAsync(id);
            if (userExist == null)
                throw new NotFoundException();

            return _mapper.Map<ResponseUserViewModel>(userExist);
        }

        public async Task<IEnumerable<ResponseUserViewModel>> GetParamsAsync(UserParams userParams, int? skip = null, int? take = null)
        {
            return _mapper.Map<IEnumerable<ResponseUserViewModel>>(await _userRepository.GetAllAsync(filter: userParams.Filter(), skip: skip, take: take));
        }

        public async Task<int> CountAsync(UserParams userParams)
        {
            return await _userRepository.CountAsync(userParams.Filter());
        }

        public IEnumerable<UserRoleViewModel> GetUserRole()
        {
            return _mapper.Map<IEnumerable<UserRoleViewModel>>(Enumeration.GetAll<UserRole>());
        }

        public async Task<ResponseUserViewModel> GetOwnUserAsync()
        {
            var userExist = await _userRepository.GetByIdAsync(_authService.Id);
            if (userExist == null)
                throw new NotFoundException();

            return _mapper.Map<ResponseUserViewModel>(userExist);
        }
    }
}
