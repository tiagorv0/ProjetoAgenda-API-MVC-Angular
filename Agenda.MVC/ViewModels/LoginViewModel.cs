using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Agenda.MVC.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} é obrigatório")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DisplayName("Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
