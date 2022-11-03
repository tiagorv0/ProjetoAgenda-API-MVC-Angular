using Agenda.MVC.Areas.Admin.Data;
using Agenda.MVC.Areas.Admin.ViewModels;
using Agenda.MVC.Constants;
using Agenda.MVC.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class LogController : Controller
    {
        private readonly IApiLogService _apiLogService;

        public LogController(IApiLogService apiLogService)
        {
            _apiLogService = apiLogService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var pageParams = new PageParams<InteractionGetViewModel>();
            var pagination = pageParams.RealizePagination(await _apiLogService.GetInteractionAsync(), page);

            return View(pagination);
        }
    }
}
