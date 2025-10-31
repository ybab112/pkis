using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;

namespace MyWebApp.Controllers
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

        [HttpPost]
        public IActionResult SubmitForm(UserModel user)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = $"Спасибо, {user.Name}! Мы получили вашу заявку.";
                return View("Index");
            }
            return View("Index");
        }
    }
}