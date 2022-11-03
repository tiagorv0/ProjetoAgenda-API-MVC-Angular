using Agenda.MVC.Areas.Admin.ViewModels;
using Agenda.MVC.ViewModels;
using Refit;

namespace Agenda.MVC.Areas.Admin.Data
{
    [Headers("Authorization: Bearer")]
    public interface IApiLogService
    {
        [Get("/admin/log")]
        Task<IEnumerable<InteractionGetViewModel>> GetInteractionAsync();

        [Get("/admin/log/types")]
        Task<IEnumerable<EnumerationViewModel>> GetInteractionTypesAsync();

        [Get("/admin/log/save-json")]
        Task DownloadInteractions();
    }
}
