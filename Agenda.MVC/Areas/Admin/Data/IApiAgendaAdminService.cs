using Agenda.MVC.Areas.Admin.ViewModels;
using Agenda.MVC.Data;
using Agenda.MVC.ViewModels;
using Refit;

namespace Agenda.MVC.Areas.Admin.Data
{
    [Headers("Authorization: Bearer")]
    public interface IApiAgendaAdminService
    {
        [Get("/admin/agenda/{id}")]
        Task<ContactAdminGetViewModel> GetContactByIdAsync(int id);

        [Get("/admin/agenda/search")]
        Task<IEnumerable<ContactAdminGetViewModel>> GetAllContactsAsync(object search);

        [Get("/admin/agenda/phone-types")]
        Task<IEnumerable<EnumerationViewModel>> GetPhoneTypesAsync();

        [Post("/admin/agenda")]
        Task<ApiBaseReponse<ContactAdminGetViewModel>> CreateContactAsync(ContactAdminPostViewModel contactPostViewModel);

        [Put("/admin/agenda/{id}")]
        Task<ApiBaseReponse<ContactAdminGetViewModel>> UpdateContactAsync(int id, ContactAdminPostViewModel contactPostViewModel);

        [Delete("/admin/agenda/{id}")]
        Task RemoveContactAsync(int id);
    }
}
