using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Agenda.MVC.ViewModels
{
    public class PhonePostViewModel
    {
        [DisplayName("Tipo de Telefone")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public int PhoneTypeId { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DisplayName("Descrição")]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DisplayName("Número do Telefone")]
        [RegularExpression(@"^\([1-9]{2}\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}$", ErrorMessage = "Telefone Inválido")]
        public string FormattedPhone { get; set; }
    }
}
