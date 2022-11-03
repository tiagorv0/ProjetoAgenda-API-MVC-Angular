using Agenda.Application.ViewModels.Contact;

namespace Agenda.Application.ViewModels.AdminContact
{
    public class RequestAdminContactViewModel : RequestContactViewModel
    {
        public int UserId { get; set; }
    }
}
