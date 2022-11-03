using Agenda.Application.ViewModels.Phone;
using Agenda.Domain.Core;

namespace Agenda.Application.ViewModels.Contact
{
    public class ResponseContactViewModel : Register
    {
        public string Name { get; set; }

        public IEnumerable<ResponsePhoneViewModel> Phones { get; set; } = new List<ResponsePhoneViewModel>();

    }
}
