using System.Linq.Expressions;
using Agenda.Application.Params;
using Agenda.Application.ViewModels.AdminContact;
using Agenda.Application.ViewModels.Enums;
using Agenda.Domain.Entities;

namespace Agenda.Application.Interfaces
{
    public interface IAgendaAdminService
    {
        Task<ResponseAdminContactViewModel> CreateAsync(RequestAdminContactViewModel entity);
        Task<ResponseAdminContactViewModel> UpdateAsync(RequestAdminContactViewModel entity, int id);
        Task RemoveAsync(int id);
        Task<IEnumerable<ResponseAdminContactViewModel>> GetAllAsync(Expression<Func<Contact, bool>>? expression = null, int? skip = null, int? take = null);
        Task<ResponseAdminContactViewModel> GetByIdAsync(int id);
        Task<IEnumerable<ResponseAdminContactViewModel>> GetParamsAsync(AdminContactParams contactParams, int? skip = null, int? take = null);
        IEnumerable<PhoneTypeViewModel> GetPhoneTypes();
        Task<int> CountAsync(AdminContactParams contactParams);
        Task<bool> IdExists(int id);
    }
}
