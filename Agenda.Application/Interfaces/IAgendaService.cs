using Agenda.Application.Params;
using Agenda.Application.ViewModels.Contact;
using Agenda.Application.ViewModels.Enums;
using Agenda.Domain.Entities;
using System.Linq.Expressions;

namespace Agenda.Application.Interfaces
{
    public interface IAgendaService
    {
        Task<ResponseContactViewModel> CreateAsync(RequestContactViewModel entity);
        Task<ResponseContactViewModel> UpdateAsync(RequestContactViewModel entity, int id);
        Task RemoveAsync(int id);
        Task<IEnumerable<ResponseContactViewModel>> GetAllAsync(Expression<Func<Contact, bool>>? expression = null, int? skip = null, int? take = null);
        Task<ResponseContactViewModel> GetByIdAsync(int id);
        Task<IEnumerable<ResponseContactViewModel>> GetParamsAsync(ContactParams contactParams, int? skip = null, int? take = null);
        IEnumerable<PhoneTypeViewModel> GetPhoneTypes();
        Task<int> CountAsync(ContactParams contactParams);
        Task<bool> IdExists(int id);
    }
}
