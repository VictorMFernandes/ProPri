using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProPri.Core.Constants;
using ProPri.Users.Application.Queries;

namespace ProPri.WebApp.Mvc.Managers
{
    public class PageManager
    {
        private readonly IHttpContextAccessor _context;
        private readonly IUsersQueries _usersQueries;

        public PageManager(IUsersQueries usersQueries, IHttpContextAccessor context)
        {
            _usersQueries = usersQueries;
            _context = context;
        }

        public RedirectToActionResult RedirectToMainPage()
        {
            return _usersQueries.IsAuthorized(_context.HttpContext.User, ConstData.ClaimUsersRead) ?
                new RedirectToActionResult("Index", "Users", null) :
                new RedirectToActionResult("Index", "Students", null);
        }
    }
}