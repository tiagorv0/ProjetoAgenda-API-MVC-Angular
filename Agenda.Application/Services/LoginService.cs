using System.Security.Claims;
using Agenda.Application.Exceptions;
using Agenda.Application.Interfaces;
using Agenda.Application.ViewModels.Login;
using Agenda.Domain.Interfaces;
using Agenda.Infrastructure.Utils;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<LoginViewModel> _validator;

        public LoginService(IUserRepository userRepository, IValidator<LoginViewModel> validator)
        {
            _userRepository = userRepository;
            _validator = validator;
        }

        public async Task<IEnumerable<Claim>> Login(LoginViewModel model)
        {
            var validation = await _validator.ValidateAsync(model);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            var users = await _userRepository.GetAllAsync(null ,
                u => u.Include(x => x.UserRole));
            var user = users.FirstOrDefault(x => x.UserName == model.Username);

            if(user == null)
                throw new BadRequestException(nameof(model.Username), "Usuário ou Senha inválidos");

            if (!PasswordHasher.ValidPasswordAsync(user, model.Password))
                throw new BadRequestException(nameof(model.Password), "Usuário ou Senha inválidos");

            return new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.UserRole.Name),
            };
        }
    }
}
