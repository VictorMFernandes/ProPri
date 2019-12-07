using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProPri.Users.Application.Queries;
using ProPri.WebApp.Mvc.Views.Entries.ViewModels;
using ProPri.WebApp.Mvc.Views.Users.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProPri.WebApp.Mvc.Controllers
{
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUsersQueries _authQueries;

        public UsersController(IMapper mapper, IUsersQueries authQueries)
        {
            _mapper = mapper;
            _authQueries = authQueries;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _authQueries.GetUsers();
            return View(_mapper.Map<IEnumerable<UserIndexViewModel>>(users));
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