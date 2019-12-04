using Microsoft.AspNetCore.Mvc;
using ProPri.WebApp.Mvc.Views.Entries.ViewModels;
using System;
using System.Collections.Generic;
using ProPri.WebApp.Mvc.Views.Classifications.ViewModels;

namespace ProPri.WebApp.Mvc.Controllers
{
    public class ClassificationsController : Controller
    {
        public IActionResult Index()
        {
            //var childrenToBe = new List<ClassificationIndexViewModel>
            //{
            //    new ClassificationIndexViewModel
            //    {
            //        Id = 9,
            //        ParentId = 3,
            //        Name = "I am"
            //    },
            //    new ClassificationIndexViewModel
            //    {
            //        Id = 3,
            //        ParentId = 1,
            //        Name = "You are",
            //    }
            //};

            //var childrenVocab = new List<ClassificationIndexViewModel>
            //{
            //    new ClassificationIndexViewModel
            //    {
            //        Id = 7,
            //        ParentId = 1,
            //        Name = "Noun"
            //    },
            //    new ClassificationIndexViewModel
            //    {
            //        Id = 6,
            //        ParentId = 1,
            //        Name = "Adverbs",
            //    },
            //    new ClassificationIndexViewModel
            //    {
            //        Id = 5,
            //        ParentId = 1,
            //        Name = "Verbs",
            //    },
            //    new ClassificationIndexViewModel
            //    {
            //        Id = 4,
            //        ParentId = 1,
            //        Name = "Pronouns",
            //    }
            //};

            //var childrenStructure = new List<ClassificationIndexViewModel>
            //{
            //    new ClassificationIndexViewModel
            //    {
            //        Id = 3,
            //        ParentId = 2,
            //        Name = "To Be",
            //        Children = childrenToBe,
            //        HasChild = true
            //    }
            //};

            //var classifications = new List<ClassificationIndexViewModel>
            //{
            //    new ClassificationIndexViewModel
            //    {
            //        Id = 1,
            //        ParentId = null,
            //        Name = "Vocab",
            //        Children = childrenVocab,
            //        HasChild = true
            //    },
            //    new ClassificationIndexViewModel
            //    {
            //        Id = 2,
            //        ParentId = null,
            //        Name = "Structure",
            //        Children = childrenStructure,
            //        HasChild = true
            //    }
            //};

            //ViewBag.dataSource = classifications;

            //return View();

            List<Continents> continents = new List<Continents>();
            List<Countries> countries = new List<Countries>();
            continents.Add(new Continents
            {
                code = "NA",
                name = "North America",
                expanded = true,
                child = countries,
            });
            countries.Add(new Countries { code = "USA", name = "United States of America", selected = true });
            countries.Add(new Countries { code = "CUB", name = "Cuba" });
            countries.Add(new Countries { code = "MEX", name = "Mexico" });
            List<Countries> countries2 = new List<Countries>();
            continents.Add(new Continents
            {
                code = "AF",
                name = "Africa",
                child = countries2,
            });
            countries2.Add(new Countries { code = "NGA", name = "Nygeria" });
            countries2.Add(new Countries { code = "EGY", name = "Egypt" });
            countries2.Add(new Countries { code = "ZAF", name = "South Africa" });
            List<Countries> countries3 = new List<Countries>();
            continents.Add(new Continents
            {
                code = "AS",
                name = "Asia",
                child = countries3,
            });
            countries3.Add(new Countries { code = "CHN", name = "China" });
            countries3.Add(new Countries { code = "IND", name = "India" });
            countries3.Add(new Countries { code = "JPN", name = "Japan" });
            List<Countries> countries4 = new List<Countries>();
            continents.Add(new Continents
            {
                code = "EU",
                name = "Europe",
                child = countries4,
            });
            countries4.Add(new Countries { code = "DNK", name = "Denmark" });
            countries4.Add(new Countries { code = "FIN", name = "Finland" });
            countries4.Add(new Countries { code = "AUT", name = "Austria" });

            List<Countries> countries5 = new List<Countries>();
            continents.Add(new Continents
            {
                code = "SA",
                name = "South America",
                child = countries5,
            });
            countries5.Add(new Countries { code = "BRA", name = "Brazil" });
            countries5.Add(new Countries { code = "COL", name = "Colombia" });
            countries5.Add(new Countries { code = "ARG", name = "Argentina" });

            List<Countries> countries6 = new List<Countries>();
            continents.Add(new Continents
            {
                code = "OC",
                name = "Oceania",
                child = countries6,
            });
            countries6.Add(new Countries { code = "AUS", name = "Australia" });
            countries6.Add(new Countries { code = "NZL", name = "Newzealand" });
            countries6.Add(new Countries { code = "WSM", name = "Samoa" });

            List<Countries> countries7 = new List<Countries>();
            continents.Add(new Continents
            {
                code = "AN",
                name = "Antartica",
                child = countries7,
            });
            countries7.Add(new Countries { code = "BVT", name = "Bouvet Island" });
            countries7.Add(new Countries { code = "ATF", name = "French Southern Lands" });

            char[] value = { 'c', 'h', 'i', 'l', 'd' };
            string Child = new string(value);
            ViewBag.child = Child;
            ViewBag.data = continents;
            return View();
        }

        public class Continents
        {
            public string code;
            public string name;
            public bool expanded;
            public bool selected;
            public List<Countries> child;

        }
        public class Countries
        {
            public string code;
            public string name;
            public bool expanded;
            public bool selected;

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