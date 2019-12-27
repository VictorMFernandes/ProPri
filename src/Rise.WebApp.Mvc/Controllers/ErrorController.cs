using Microsoft.AspNetCore.Mvc;
using Rise.Core.WebApp.Constants;

namespace Rise.WebApp.Mvc.Controllers
{
    [Route("Error")]
    public class ErrorController : Controller
    {
        [Route(ConstStrings.HttpCodeInternalError)]
        public IActionResult InternalServerError()
        {
            return View(ConstStrings.HttpCodeInternalError);
        }

        [Route(ConstStrings.HttpCodeNotFound)]
        public IActionResult PageNotFound()
        {
            return View(ConstStrings.HttpCodeNotFound);
        }

        [Route(ConstStrings.HttpCodeForbidden)]
        public IActionResult Forbidden()
        {
            return View(ConstStrings.HttpCodeForbidden);
        }
    }
}