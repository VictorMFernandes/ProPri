using Microsoft.AspNetCore.Mvc;
using ProPri.WebApp.Mvc.Views.Autorizacao.ViewModels;

namespace ProPri.WebApp.Mvc.Controllers
{
    public class AutorizacaoController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginVm)
        {
            return View();
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }

        public IActionResult RecuperarSenha(LoginViewModel loginVm)
        {
            return View(loginVm);
        }

        [HttpPost]
        public IActionResult RecuperarSenha(string email)
        {
            return View();
        }
    }
}