using Microsoft.AspNetCore.Mvc;

namespace ProPri.WebApp.Api.Controllers
{
    public class ExemploController : Controller
    {
        [Route("Exemplo/Index")]
        public IActionResult Index()
        {
            return Json(new { teste = "Funcionou" });
        }
    }
}