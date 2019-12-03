using Microsoft.AspNetCore.Mvc;
using ProPri.WebApp.Mvc.Views.Palavras.ViewModels;
using System;

namespace ProPri.WebApp.Mvc.Controllers
{
    public class PalavrasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete(Guid id)
        {
            // Pegar palavra
            // Checar se é null
            return PartialView("_Delete", new PalavraIndexViewModel { Id = id });
        }

        [HttpPost]
        public IActionResult Delete(PalavraIndexViewModel palavraIndexVm)
        {
            var url = Url.Action("Index", "Palavras");
            return Json(new { success = true, url });
        }
    }
}