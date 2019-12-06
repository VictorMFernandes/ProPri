using Microsoft.AspNetCore.Mvc;
using ProPri.WebApp.Mvc.Views.Entries.ViewModels;
using System;
using ProPri.WebApp.Mvc.Views.Students.ViewModels;

namespace ProPri.WebApp.Mvc.Controllers
{
    public class StudentsController : Controller
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
            var studentFormVm = new StudentFormViewModel
            {
                Name = "Matilda"
            };

            return View(studentFormVm);
        }

        public IActionResult Delete(Guid id)
        {
            // Pegar palavra
            // Checar se é null
            return PartialView("_Delete", new EntryIndexViewModel { Id = id });
        }

        [HttpPost]
        public IActionResult Delete(EntryIndexViewModel entryIndexVm)
        {
            var url = Url.Action("Index", "Palavras");
            return Json(new { success = true, url });
        }
    }
}