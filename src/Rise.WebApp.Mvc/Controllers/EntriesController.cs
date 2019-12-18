using Microsoft.AspNetCore.Mvc;
using Rise.WebApp.Mvc.Views.Classifications.ViewModels;
using Rise.WebApp.Mvc.Views.Entries.ViewModels;
using System;
using System.Collections.Generic;

namespace Rise.WebApp.Mvc.Controllers
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
                Image = "https://cdn.cnn.com/cnnnext/dam/assets/191024091949-02-foster-cat-large-169.jpg",
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