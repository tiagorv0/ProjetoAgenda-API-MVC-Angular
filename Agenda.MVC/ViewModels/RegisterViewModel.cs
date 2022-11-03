using System.ComponentModel;

namespace Agenda.MVC.ViewModels
{
    public class RegisterViewModel
    {
        public int Id { get; set; }

        [DisplayName("Criado em")]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Última Atualização")]
        public DateTime? UpdatedAt { get; set; }
    }
}
