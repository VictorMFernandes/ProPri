using Microsoft.AspNetCore.Mvc;
using ProPri.WebApp.Mvc.Views.Classifications.ViewModels;
using ProPri.WebApp.Mvc.Views.Entries.ViewModels;
using System;
using System.Collections.Generic;

namespace ProPri.WebApp.Mvc.Controllers
{
    public class EntriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var classifications = new List<ClassificationIndexViewModel>
            {
                new ClassificationIndexViewModel{ Id = 1, Name = "Noun"},
                new ClassificationIndexViewModel{ Id = 2, Name = "Adverb"},
                new ClassificationIndexViewModel{ Id = 3, Name = "Verb"},
                new ClassificationIndexViewModel{ Id = 4, Name = "Pronoun"}
            };

            var entryFormVm = new EntryFormViewModel
            {
                Classifications = classifications
            };

            return View(entryFormVm);
        }

        public IActionResult Edit()
        {
            var classifications = new List<ClassificationIndexViewModel>
            {
                new ClassificationIndexViewModel{ Id = 1, Name = "Noun"},
                new ClassificationIndexViewModel{ Id = 2, Name = "Adverb"},
                new ClassificationIndexViewModel{ Id = 3, Name = "Verb"},
                new ClassificationIndexViewModel{ Id = 4, Name = "Pronoun"}
            };

            var entry = new EntryFormViewModel
            {
                Image = "https://www.petz.com.br/blog/wp-content/uploads/2019/06/tamanho-de-gato.jpg",
                English = "Cat",
                Portuguese = "Gato",
                Classifications = classifications
            };

            return View(entry);
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