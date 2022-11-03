using Agenda.MVC.ViewModels;
using Refit;

namespace Agenda.MVC.Data
{
    [Headers("Authorization: Bearer")]
    public interface IApiAgendaService
    {
        [Get("/agenda/{id}")]
        Task<ContactGetViewModel> GetContactByIdAsync(int id);

        [Get("/agenda/search")]
        Task<IEnumerable<ContactGetViewModel>> GetAllContactsAsync(object search);

        [Get("/agenda/phone-types")]
        Task<IEnumerable<EnumerationViewModel>> GetPhoneTypesAsync();

        [Post("/agenda")]
        Task<ApiBaseReponse<ContactGetViewModel>> CreateContactAsync(ContactPostViewModel contactPostViewModel);

        [Put("/agenda/{id}")]
        Task<ApiBaseReponse<ContactGetViewModel>> UpdateContactAsync(int id, ContactPostViewModel contactPostViewModel);

        [Delete("/agenda/{id}")]
        Task RemoveContactAsync(int id);
    }
}
