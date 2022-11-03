using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Agenda.MVC.ViewModels;

namespace Agenda.MVC.Areas.Admin.ViewModels
{
    public class ContactAdminPostViewModel : ContactPostViewModel
    {
        [Required(ErrorMessage = "{0} é obrigatório")]
        [DisplayName("Usuário")]
        public int UserId { get; set; }
    }
}
