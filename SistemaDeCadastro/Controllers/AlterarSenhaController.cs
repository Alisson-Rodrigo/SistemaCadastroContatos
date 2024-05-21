using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Controllers{
    public class AlterarSenhaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Alterar(AlterarSenhaModel alterarSenha)
        {
            if (ModelState.IsValid)
            {
                //alterar senha
                return RedirectToAction("Index", "Home");
            }
            TempData["MensagemErro"] = "Ops, não conseguimos alterar a sua senha, verifique os campos e tente novamente.";
            return View("Index", alterarSenha);
        }
    }
}