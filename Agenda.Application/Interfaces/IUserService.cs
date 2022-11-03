using System.Linq.Expressions;
using Agenda.Application.Params;
using Agenda.Application.ViewModels.Enums;
using Agenda.Application.ViewModels.User;
using Agenda.Domain.Entities;

namespace Agenda.Application.Interfaces
{
    public interface IUserService
    {
        Task<ResponseUserViewModel> CreateAsync(RequestUserViewModel entity);
        Task<ResponseUserViewModel> UpdateAsync(RequestUserViewModel entity, int id);
        Task RemoveAsync(int id);
        Task<IEnumerable<ResponseUserViewModel>> GetAllAsync(Expression<Func<User, bool>>? expression = null, int? skip = null, int? take = null);
        Task<ResponseUserViewModel> GetByIdAsync(int id);
        Task<IEnumerable<ResponseUserViewModel>> GetParamsAsync(UserParams userParams, int? skip = null, int? take = null);
        Task<int> CountAsync(UserParams userParams);
        IEnumerable<UserRoleViewModel> GetUserRole();
        Task<ResponseUserViewModel> GetOwnUserAsync();
    }
}
