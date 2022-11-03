using System.ComponentModel;
using Agenda.MVC.ViewModels;

namespace Agenda.MVC.Areas.Admin.ViewModels
{
    public class UserGetViewModel : RegisterViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }

        [DisplayName("Usuário")]
        public string UserName { get; set; }
        public EnumerationViewModel UserRole { get; set; }
    }
}
