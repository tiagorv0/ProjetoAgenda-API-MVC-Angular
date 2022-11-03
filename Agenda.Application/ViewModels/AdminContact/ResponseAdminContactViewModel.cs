using Agenda.Application.ViewModels.Contact;
using Agenda.Application.ViewModels.User;

namespace Agenda.Application.ViewModels.AdminContact
{
    public class ResponseAdminContactViewModel : ResponseContactViewModel
    {
        public ResponseUserViewModel User { get; set; }
    }
}
