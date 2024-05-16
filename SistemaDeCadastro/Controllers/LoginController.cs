using Microsoft.AspNetCore.Mvc;

namespace SistemaDeCadastro.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}