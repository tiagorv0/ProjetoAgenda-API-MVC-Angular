using System.ComponentModel.DataAnnotations;

namespace Agenda.Application.ViewModels.Phone
{
    public class RequestPhoneViewModel
    {
        public int PhoneTypeId { get; set; }

        public string Description { get; set; }

        public string FormattedPhone { get; set; }
    }
}
