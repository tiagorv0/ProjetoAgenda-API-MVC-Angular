using Agenda.MVC.ViewModels;

namespace Agenda.MVC.Areas.Admin.ViewModels
{
    public class ContactAdminGetViewModel : ContactGetViewModel
    {
        public UserGetViewModel User { get; set; }
    }
}
