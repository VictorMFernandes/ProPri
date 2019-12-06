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
                    Name = "I am"
                },
                new ClassificationIndexViewModel
                {
                    Id = 10,
                    Name = "He is",
                },
                new ClassificationIndexViewModel
                {
                    Id = 11,
                    Name = "She is",
                },
                new ClassificationIndexViewModel
                {
                    Id = 12,
                    Name = "It is",
                },
                new ClassificationIndexViewModel
                {
                    Id = 13,
                    Name = "You are",
                },
                new ClassificationIndexViewModel
                {
                    Id = 14,
                    Name = "We are",
                },
                new ClassificationIndexViewModel
                {
                    Id = 15,
                    Name = "We are",
                },
                new ClassificationIndexViewModel
                {
                    Id = 16,
                    Name = "We are",
                }
            };

            var childrenVocab = new List<ClassificationIndexViewModel>
            {
                new ClassificationIndexViewModel
                {
                    Id = 7,
                    Name = "Noun"
                },
                new ClassificationIndexViewModel
                {
                    Id = 6,
                    Name = "Adverbs",
                },
                new ClassificationIndexViewModel
                {
                    Id = 5,
                    Name = "Verbs",
                },
                new ClassificationIndexViewModel
                {
                    Id = 4,
                    Name = "Pronouns",
                },
                new ClassificationIndexViewModel
                {
                    Id = 17,
                    Name = "Pronouns",
                },
                new ClassificationIndexViewModel
                {
                    Id = 18,
                    Name = "Pronouns",
                },
                new ClassificationIndexViewModel
                {
                    Id = 19,
                    Name = "Pronouns",
                },
                new ClassificationIndexViewModel
                {
                    Id = 20,
                    Name = "Pronouns",
                },
                new ClassificationIndexViewModel
                {
                    Id = 21,
                    Name = "Pronouns",
                },
                new ClassificationIndexViewModel
                {
                    Id = 22,
                    Name = "Pronouns",
                }
            };

            var childrenStructure = new List<ClassificationIndexViewModel>
            {
                new ClassificationIndexViewModel
                {
                    Id = 3,
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
                    Name = "Vocab",
                    Children = childrenVocab,
                    HasChild = true
                },
                new ClassificationIndexViewModel
                {
                    Id = 2,
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