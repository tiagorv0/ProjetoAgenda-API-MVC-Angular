using System.ComponentModel.DataAnnotations;

namespace Agenda.MVC.ViewModels
{
    public class ContactPostViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        public List<PhonePostViewModel> Phones { get; set; } = new List<PhonePostViewModel>();
    }
}
