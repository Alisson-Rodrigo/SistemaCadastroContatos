using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastro.Models;
using SistemaDeCadastro.Repositorio;

namespace SistemaDeCadastro.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public LoginController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio ?? throw new ArgumentNullException(nameof(usuarioRepositorio));
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel dadosLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = _usuarioRepositorio.BuscarPorLogin(dadosLogin.Login);
                    if (usuario != null)
                    {
                        if (usuario.VerificarSenha(dadosLogin.Senha))
                        {
                            TempData["MensagemSucesso"] = "Login efetuado com sucesso";
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    TempData["MensagemError"] = "Usuário ou senha inválidos";
                }
                return View("Index");
            }
            catch (Exception e)
            {
                TempData["MensagemError"] = $"Ops, não conseguimos logar, detalhe do erro: {e.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}
