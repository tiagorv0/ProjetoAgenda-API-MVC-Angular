using System.ComponentModel.DataAnnotations;
using Agenda.Application.ViewModels.Enums;
using Agenda.Domain.Core;

namespace Agenda.Application.ViewModels.Phone
{
    public class ResponsePhoneViewModel : Register
    {
        public PhoneTypeViewModel PhoneType { get; set; }

        public string Description { get; set; }

        public string FormattedPhone { get; set; }

        public string Number { get; set; }

        public int DDD { get; set; }
    }
}
