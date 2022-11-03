using Agenda.MVC.Areas.Admin.Data;
using Agenda.MVC.Areas.Admin.ViewModels;
using Agenda.MVC.Constants;
using Agenda.MVC.Params;
using Agenda.MVC.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agenda.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class AgendaAdminController : Controller
    {
        private readonly IApiAgendaAdminService _apiAgendaAdminService;
        private readonly IMapper _mapper;
        private readonly IApiUserService _apiUserService;

        public AgendaAdminController(IApiAgendaAdminService apiAgendaAdminService, IMapper mapper, IApiUserService apiUserService)
        {
            _apiAgendaAdminService = apiAgendaAdminService;
            _mapper = mapper;
            _apiUserService = apiUserService;
        }

        public async Task<IActionResult> Index(PageParams<ContactAdminGetViewModel> pageParams, int page = 1)
        {
            var pagination = pageParams.RealizePagination(await _apiAgendaAdminService.GetAllContactsAsync(pageParams.QueryContactToRefit()), page);

            return View(pagination);
        }

        public async Task<IActionResult> Details(int id)
        {
            var contact = await _apiAgendaAdminService.GetContactByIdAsync(id);
            return View(contact);
        }

        public async Task<IActionResult> Form(int? id = null)
        {
            await GetUsersSelectList();
            await GetPhoneTypesSelectList();

            if (!id.HasValue)
                return View(new ContactAdminPostViewModel());

            var contact = await _apiAgendaAdminService.GetContactByIdAsync((int)id);
            if (contact == null)
                return NotFound();

            return View(_mapper.Map<ContactAdminPostViewModel>(contact));
        }

        [HttpPost]
        public async Task<IActionResult> Form(ContactAdminPostViewModel viewModel, string option = "Save")
        {
            if (option.Contains("AddPhone"))
            {
                ModelState.Clear();
                viewModel.Phones.Add(new PhonePostViewModel());
            }

            if (option.Contains("RemovePhone"))
            {
                var split = option.Split("|");
                ModelState.Clear();
                viewModel.Phones.Remove(viewModel.Phones[int.Parse(split[1])]);
            }

            if (option.Contains("Save"))
            {
                var result = viewModel.Id.HasValue ?
                    await _apiAgendaAdminService.UpdateContactAsync((int)viewModel.Id, viewModel) :
                    await _apiAgendaAdminService.CreateContactAsync(viewModel);

                if (result.HasError)
                    result.AddErrorsToModelState(ModelState);
                else
                {
                    TempData["success"] = "Contato Criado com Sucesso !";
                    return RedirectToAction(nameof(Index));
                }
            }

            await GetUsersSelectList();
            await GetPhoneTypesSelectList();
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _apiAgendaAdminService.GetContactByIdAsync(id);
            if (contact == null)
            {
                ModelState.AddModelError("", "Contato não encontrado!");
                return View(nameof(Index));
            }

            return View(contact);
        }

        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiAgendaAdminService.RemoveContactAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task GetPhoneTypesSelectList()
        {
            var phoneTypes = await _apiAgendaAdminService.GetPhoneTypesAsync();
            var list = new SelectList(phoneTypes, "Id", "Name", "--Selecione--");
            ViewBag.PhoneTypes = list;
        }

        private async Task GetUsersSelectList()
        {
            var users = await _apiUserService.GetAllUsersAsync();
            var usersSelect = users.Select(x => new
            {
                Id = x.Id,
                Name = $"{x.Name} - {x.UserName}"
            });

            ViewBag.Users = new SelectList(usersSelect, "Id", "Name", "--Selecione--");
        }
    }
}
