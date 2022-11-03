using System.ComponentModel;

namespace Agenda.MVC.ViewModels
{
    public class PhoneGetViewModel : RegisterViewModel
    {
        public EnumerationViewModel PhoneType { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Telefone")]
        public string FormattedPhone { get; set; }
    }
}
