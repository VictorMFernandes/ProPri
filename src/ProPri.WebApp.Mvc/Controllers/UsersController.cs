using Microsoft.AspNetCore.Mvc;
using ProPri.WebApp.Mvc.Views.Entries.ViewModels;
using ProPri.WebApp.Mvc.Views.Users.ViewModels;
using System;
using System.Collections.Generic;

namespace ProPri.WebApp.Mvc.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var roles = new List<RoleIndexViewModel>
            {
                new RoleIndexViewModel{ Id = Guid.NewGuid(), Name = "Admin"},
                new RoleIndexViewModel{ Id = Guid.NewGuid(), Name = "Teacher"},
                new RoleIndexViewModel{ Id = Guid.NewGuid(), Name = "Secretary"}
            };

            var userFormVm = new UserFormViewModel
            {
                Roles = roles
            };

            return View(userFormVm);
        }

        public IActionResult Edit()
        {
            var roles = new List<RoleIndexViewModel>
            {
                new RoleIndexViewModel{ Id = Guid.NewGuid(), Name = "Admin"},
                new RoleIndexViewModel{ Id = Guid.NewGuid(), Name = "Teacher"},
                new RoleIndexViewModel{ Id = Guid.NewGuid(), Name = "Secretary"}
            };

            var userFormVm = new UserFormViewModel
            {
                Name = "John Doe",
                Roles = roles
            };

            return View(userFormVm);
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