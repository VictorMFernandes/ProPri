using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProPri.Core.Communication.Handlers;
using ProPri.Core.Communication.Messages.Common.Notifications;
using ProPri.Users.Application.Commands;
using ProPri.Users.Application.Queries;
using ProPri.Users.Application.Queries.Filters;
using ProPri.WebApp.Mvc.Views.Entries.ViewModels;
using ProPri.WebApp.Mvc.Views.Users.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ProPri.WebApp.Mvc.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUsersQueries _usersQueries;
        private readonly IMediatorHandler _mediatorHandler;

        public UsersController(INotificationHandler<DomainNotification> notifications,
                               IMapper mapper, IUsersQueries usersQueries,
                               IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _mapper = mapper;
            _usersQueries = usersQueries;
            _mediatorHandler = mediatorHandler;
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

        [HttpPost]
        public async Task<IActionResult> Edit(UserFormViewModel userFormVm)
        {
            userFormVm.UserId = LoggedUserId;
            await _mediatorHandler.SendCommand(_mapper.Map<EditUserCommand>(userFormVm));

            if (ValidOperation())
            {
                return RedirectToAction("Index");
            }
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