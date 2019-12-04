using Microsoft.AspNetCore.Mvc;
using ProPri.WebApp.Mvc.Views.Classifications.ViewModels;
using ProPri.WebApp.Mvc.Views.Entries.ViewModels;
using System;
using System.Collections.Generic;

namespace ProPri.WebApp.Mvc.Controllers
{
    public class ClassificationsController : Controller
    {
        public IActionResult Index()
        {
            var childrenToBe = new List<ClassificationIndexViewModel>
            {
                new ClassificationIndexViewModel
                {
                    Id = 9,
                    ParentId = 3,
                    Name = "I am"
                },
                new ClassificationIndexViewModel
                {
                    Id = 3,
                    ParentId = 1,
                    Name = "You are",
                }
            };

            var childrenVocab = new List<ClassificationIndexViewModel>
            {
                new ClassificationIndexViewModel
                {
                    Id = 7,
                    ParentId = 1,
                    Name = "Noun"
                },
                new ClassificationIndexViewModel
                {
                    Id = 6,
                    ParentId = 1,
                    Name = "Adverbs",
                },
                new ClassificationIndexViewModel
                {
                    Id = 5,
                    ParentId = 1,
                    Name = "Verbs",
                },
                new ClassificationIndexViewModel
                {
                    Id = 4,
                    ParentId = 1,
                    Name = "Pronouns",
                }
            };

            var childrenStructure = new List<ClassificationIndexViewModel>
            {
                new ClassificationIndexViewModel
                {
                    Id = 3,
                    ParentId = 2,
                    Name = "To Be",
                    Children = childrenToBe,
                    HasChild = true
                }
            };

            var classifications = new List<ClassificationIndexViewModel>
            {
                new ClassificationIndexViewModel
                {
                    Id = 1,
                    ParentId = null,
                    Name = "Vocab",
                    Children = childrenVocab,
                    HasChild = true
                },
                new ClassificationIndexViewModel
                {
                    Id = 2,
                    ParentId = null,
                    Name = "Structure",
                    Children = childrenStructure,
                    HasChild = true
                }
            };

            return View(classifications);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            var classificationFormVm = new ClassificationFormViewModel
            {
                Name = "Noun"
            };
            return View(classificationFormVm);
        }

        public IActionResult Delete(Guid id)
        {
            // Pegar palavra
            // Checar se é null
            return PartialView("_Delete", new ClassificationIndexViewModel { Id = 1 });
        }

        [HttpPost]
        public IActionResult Delete(EntryIndexViewModel entryIndexVm)
        {
            var url = Url.Action("Index", "Palavras");
            return Json(new { success = true, url });
        }
    }
}