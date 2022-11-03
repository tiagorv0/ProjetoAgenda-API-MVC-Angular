using Agenda.MVC.Areas.Admin.Data;
using Agenda.MVC.Areas.Admin.ViewModels;
using Agenda.MVC.Constants;
using Agenda.MVC.Params;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agenda.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class UserController : Controller
    {
        private readonly IApiUserService _apiUserService;
        private readonly IMapper _mapper;

        public UserController(IApiUserService apiUserService, IMapper mapper)
        {
            _apiUserService = apiUserService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var pageParams = new PageParams<UserGetViewModel>();
            var pagination = pageParams.RealizePagination(await _apiUserService.GetAllUsersAsync(), page);
            return View(pagination);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _apiUserService.GetUserByIdAsync(id);
            return View(user);
        }

        public async Task<IActionResult> Create()
        {
            await SetUserRoles();
            return View(new UserPostViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserPostViewModel viewModel)
        {
            var response = await _apiUserService.CreateUserAsync(viewModel);

            if (response.HasError)
            {
                await SetUserRoles();
                response.AddErrorsToModelState(ModelState);

                return View(viewModel);
            }

            TempData["success"] = "Usuário Criado com Sucesso !";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _apiUserService.GetUserByIdAsync(id);
            if (response == null)
            {
                await SetUserRoles();
                ModelState.AddModelError("", "Usuário não encontrado!");
                return View(response);
            }

            await SetUserRoles();
            return View(_mapper.Map<UserPostViewModel>(response));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserPostViewModel viewModel)
        {
            var response = await _apiUserService.UpdateUserAsync(id, viewModel);

            if (response.HasError)
            {
                await SetUserRoles();
                response.AddErrorsToModelState(ModelState);

                return View(response.Content);
            }

            TempData["success"] = "Usuário Alterado com Sucesso !";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _apiUserService.GetUserByIdAsync(id);
            if (user == null)
            {
                ModelState.AddModelError("", "Usuário não encontrado!");
                return View(nameof(Index));
            }

            return View(user);
        }

        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TempData["success"] = "Usuário Removido com Sucesso !";
            await _apiUserService.RemoveUserAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task SetUserRoles()
        {
            var roles = await _apiUserService.GetUserRolesAsync();
            var list = new SelectList(roles, "Id", "Name", "--Selecione--");
            ViewBag.roles = list;
        }
    }
}
