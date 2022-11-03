using Agenda.Application.ViewModels.Phone;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Application.ViewModels.Contact
{
    public  class RequestContactViewModel
    {
        public string Name { get; set; }

        public IEnumerable<RequestPhoneViewModel> Phones { get; set; }
    }
}
