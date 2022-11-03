using Agenda.MVC.Data;
using Agenda.MVC.Params;
using Agenda.MVC.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agenda.MVC.Controllers
{
    [Authorize]
    public class AgendaController : Controller
    {
        private readonly IApiAgendaService _apiAgendaService;
        private readonly IMapper _mapper;

        public AgendaController(IApiAgendaService apiAgendaService, IMapper mapper)
        {
            _apiAgendaService = apiAgendaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(PageParams<ContactGetViewModel> pageParams, int page = 1)
        {
            var pagination = pageParams.RealizePagination(await _apiAgendaService.GetAllContactsAsync(pageParams.QueryContactToRefit()), page);

            return View(pagination);
        }

        public async Task<IActionResult> Details(int id)
        {
            var contact = await _apiAgendaService.GetContactByIdAsync(id);
            return View(contact);
        }

        public async Task<IActionResult> Form(int? id = null)
        {
            await GetPhoneTypes();

            if (!id.HasValue)
                return View(new ContactPostViewModel());

            var contact = await _apiAgendaService.GetContactByIdAsync((int)id);
            if (contact == null)
                return NotFound();

            return View(_mapper.Map<ContactPostViewModel>(contact));
        }

        [HttpPost]
        public async Task<IActionResult> Form(ContactPostViewModel viewModel, string option = "Save")
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
                    await _apiAgendaService.UpdateContactAsync((int)viewModel.Id, viewModel) :
                    await _apiAgendaService.CreateContactAsync(viewModel);

                if (result.HasError)
                    result.AddErrorsToModelState(ModelState);
                else
                {
                    TempData["success"] = "Contato Criado com Sucesso !";
                    return RedirectToAction(nameof(Index));
                }
            }

            await GetPhoneTypes();
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _apiAgendaService.GetContactByIdAsync(id);
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
            await _apiAgendaService.RemoveContactAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task GetPhoneTypes()
        {
            var phoneTypes = await _apiAgendaService.GetPhoneTypesAsync();
            var list = new SelectList(phoneTypes, "Id", "Name", "--Selecione--");
            ViewBag.PhoneTypes = list;
        }
    }
}
