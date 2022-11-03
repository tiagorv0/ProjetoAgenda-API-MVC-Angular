namespace Agenda.Application.Interfaces
{
    public interface IRulesValidation
    {
        void GetIdsForValidation(int userId, int? contactId = null);
        Task<bool> ExistFormattedPhoneAsync(string phone,
            CancellationToken cancelToken = default);
        Task<bool> ExistUserIdAsync(int id,
            CancellationToken cancelToken = default);
        Task<bool> ExistUsernameAsync(string userName,
            CancellationToken cancelToken = default);
        Task<bool> ExistEmailAsync(string email,
            CancellationToken cancelToken = default);
    }
}
