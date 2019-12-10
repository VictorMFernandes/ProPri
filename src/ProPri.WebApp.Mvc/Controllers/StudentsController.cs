using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProPri.Core.Constants;
using ProPri.WebApp.Mvc.Views.Entries.ViewModels;
using ProPri.WebApp.Mvc.Views.Students.ViewModels;
using System;

namespace ProPri.WebApp.Mvc.Controllers
{
    public class StudentsController : Controller
    {
        [Authorize(Policy = ConstData.ClaimStudentsRead)]
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