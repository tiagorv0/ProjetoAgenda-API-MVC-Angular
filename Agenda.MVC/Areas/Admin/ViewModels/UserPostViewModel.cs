using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Agenda.MVC.Areas.Admin.ViewModels
{
    public class UserPostViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Por favor, insira um {0} válido")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DisplayName("Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DisplayName("Cargo")]
        public int UserRoleId { get; set; }
    }
}
