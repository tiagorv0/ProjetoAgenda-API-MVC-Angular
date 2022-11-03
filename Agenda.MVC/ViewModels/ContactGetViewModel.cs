using System.ComponentModel;

namespace Agenda.MVC.ViewModels
{
    public class ContactGetViewModel : RegisterViewModel
    {
        [DisplayName("Nome")]
        public string Name { get; set; }
        public List<PhoneGetViewModel> Phones { get; set; }
    }
}
