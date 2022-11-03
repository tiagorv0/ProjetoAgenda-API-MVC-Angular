using Agenda.Application.Interfaces;
using Agenda.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Application.Utils
{
    public class RulesValidation : IRulesValidation
    {
        private readonly IUserRepository _userRepository;
        private readonly IContactRepository _contactRepository;
        private int _userId;
        private int? _contactId;

        public RulesValidation(IUserRepository userRepository, IContactRepository contactRepository)
        {
            _userRepository = userRepository;
            _contactRepository = contactRepository;
        }

        public void GetIdsForValidation(int userId, int? contactId = null)
        {
            _userId = userId;
            _contactId = contactId;
        }

        public async Task<bool> ExistFormattedPhoneAsync(string phone,
            CancellationToken cancelToken = default)
        {
            var contacts = await _contactRepository.GetAllAsync(x => x.UserId == _userId, x => x.Include(p => p.Phones));
            var phones = contacts.Where(x => x.Id != _contactId).SelectMany(p => p.Phones);
            return !phones.Any(x => x.FormattedPhone == phone);
        }

        public async Task<bool> ExistUserIdAsync(int id,
            CancellationToken cancelToken = default)
        {
            var users = await _userRepository.GetAllAsync();
            return users.Any(x => x.Id == id);
        }

        public async Task<bool> ExistUsernameAsync(string userName,
            CancellationToken cancelToken = default)
        {
            var users = await _userRepository.GetAllAsync(x => x.UserName == userName);
            return users.All(x => x.Id == _userId);
        }

        public async Task<bool> ExistEmailAsync(string email,
            CancellationToken cancelToken = default)
        {
            var usersContext = await _userRepository.GetAllAsync(x => x.Email == email);
            return usersContext.All(x => x.Id == _userId);
        }
    }
}
