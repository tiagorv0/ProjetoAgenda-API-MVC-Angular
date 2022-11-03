using System.Diagnostics;
using Agenda.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error(int id)
        {
            return View();
        }
    }
}
