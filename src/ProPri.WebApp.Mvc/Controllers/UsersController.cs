using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProPri.Users.Application.Queries;
using ProPri.Users.Application.Queries.Filters;
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
        private readonly IUsersQueries _usersQueries;

        public UsersController(IMapper mapper, IUsersQueries usersQueries)
        {
            _mapper = mapper;
            _usersQueries = usersQueries;
        }

        public async Task<IActionResult> Index()
        {
            var userFilter = new UserFilter
            {
                PageNumber = 1,
                PageSize = 5
            };

            var users = await _usersQueries.GetUsers(userFilter);
            return View(_mapper.Map<IEnumerable<UserIndexViewModel>>(users));
        }

        public async Task<IActionResult> Create()
        {
            var roles = await _usersQueries.GetAllRoleIdName();

            var userFormVm = new UserFormViewModel
            {
                Roles = _mapper.Map<IEnumerable<RoleIndexViewModel>>(roles)
            };

            return View(userFormVm);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var userFormVm = _mapper.Map<UserFormViewModel>(await _usersQueries.GetUserById(id));
            var roles = await _usersQueries.GetAllRoleIdName();

            userFormVm.Roles = _mapper.Map<IEnumerable<RoleIndexViewModel>>(roles);

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