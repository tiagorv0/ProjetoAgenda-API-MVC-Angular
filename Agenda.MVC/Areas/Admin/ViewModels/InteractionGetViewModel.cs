using Agenda.MVC.ViewModels;

namespace Agenda.MVC.Areas.Admin.ViewModels
{
    public class InteractionGetViewModel : RegisterViewModel
    {
        public int UserId { get; set; }
        public int InteractionTypeId { get; set; }
        public string Message { get; set; }
    }
}
