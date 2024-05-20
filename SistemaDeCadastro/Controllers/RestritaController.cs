using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastro.Filters;

namespace RestritoController
{

    [PaginaParaUsuarioLogado]
    public class RestritaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}