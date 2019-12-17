using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProPri.Core.Communication.Handlers;
using ProPri.Core.Communication.Messages.Common.Notifications;
using ProPri.Core.Constants;
using ProPri.Users.Application.Commands;
using ProPri.Users.Application.Queries;
using ProPri.Users.Domain.Filters;
using ProPri.WebApp.Mvc.Views.Users.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProPri.Core.Helpers;

namespace ProPri.WebApp.Mvc.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUsersQueries _usersQueries;
        private readonly IMediatorHandler _mediatorHandler;

        public UsersController(INotificationHandler<DomainNotification> notifications,
                               IMapper mapper, IUsersQueries usersQueries,
                               IMediatorHandler mediatorHandler)
            : base(notifications, mediatorHandler, usersQueries)
        {
            _mapper = mapper;
            _usersQueries = usersQueries;
            _mediatorHandler = mediatorHandler;
        }

        [Authorize(Policy = ConstData.ClaimUsersRead)]
        public async Task<IActionResult> Index(string searchString, string currentFilter, EActiveFilter? activeFilter, EActiveFilter currentActiveFilter, int pageNumber = 1)
        {
            if (searchString != null || activeFilter != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
                activeFilter = currentActiveFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentActiveFilter"] = activeFilter;

            var userFilter = new UserFilter(pageNumber, 5, searchString, activeFilter);

            var users = await _usersQueries.GetUsers(userFilter);
            return View(users);
        }

        [Authorize(Policy = ConstData.ClaimUsersWrite)]
        public async Task<IActionResult> Create()
        {
            var roles = await _usersQueries.GetAllRoleIdName();

            var userFormVm = new UserFormViewModel
            {
                Active = true,
                Roles = _mapper.Map<IEnumerable<RoleIndexViewModel>>(roles)
            };

            return View(userFormVm);
        }

        [Authorize(Policy = ConstData.ClaimUsersWrite)]
        [HttpPost]
        public async Task<IActionResult> Create(UserFormViewModel userFormVm)
        {
            userFormVm.LoggedUserId = LoggedUserId;
            await _mediatorHandler.SendCommand(_mapper.Map<CreateUserCommand>(userFormVm));

            if (ValidOperation())
            {
                return RedirectToAction("Index");
            }

            var roles = await _usersQueries.GetAllRoleIdName();
            userFormVm.Roles = _mapper.Map<IEnumerable<RoleIndexViewModel>>(roles);

            return View(userFormVm);
        }

        [Authorize(Policy = ConstData.ClaimUsersWrite)]
        public async Task<IActionResult> Edit(Guid id)
        {
            var userFormVm = _mapper.Map<UserFormViewModel>(await _usersQueries.GetUserById(id));
            var roles = await _usersQueries.GetAllRoleIdName();

            userFormVm.Roles = _mapper.Map<IEnumerable<RoleIndexViewModel>>(roles);

            return View(userFormVm);
        }

        [Authorize(Policy = ConstData.ClaimUsersWrite)]
        [HttpPost]
        public async Task<IActionResult> Edit(UserFormViewModel userFormVm)
        {
            userFormVm.LoggedUserId = LoggedUserId;
            await _mediatorHandler.SendCommand(_mapper.Map<EditUserCommand>(userFormVm));

            if (ValidOperation())
            {
                return RedirectToAction("Index");
            }

            var roles = await _usersQueries.GetAllRoleIdName();
            userFormVm.Roles = _mapper.Map<IEnumerable<RoleIndexViewModel>>(roles);

            return View(userFormVm);
        }
    }
}