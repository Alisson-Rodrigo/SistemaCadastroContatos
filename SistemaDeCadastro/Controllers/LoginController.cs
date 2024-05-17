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
                    // Verifica se dadosLogin e dadosLogin.Login não são nulos
                    if (dadosLogin == null)
                    {
                        TempData["MensagemError"] = "Dados de login são nulos";
                        return View("Index");
                    }
                    if (string.IsNullOrWhiteSpace(dadosLogin.Login))
                    {
                        TempData["MensagemError"] = "O campo de login está vazio";
                        return View("Index", dadosLogin);
                    }

                    UserModel usuario = _usuarioRepositorio.BuscarPorLogin(dadosLogin.Login);
                    if (usuario == null)
                    {
                        TempData["MensagemError"] = "Usuário não encontrado";
                        return View("Index", dadosLogin);
                    }

                    if (!usuario.VerificarSenha(dadosLogin.Senha))
                    {
                        TempData["MensagemError"] = "Senha incorreta";
                        return View("Index", dadosLogin);
                    }

                    return RedirectToAction("Index", "Home");
                }
                return View("Index", dadosLogin);
            }
            catch (Exception e)
            {
                TempData["MensagemError"] = $"Ops, não conseguimos logar, detalhe do erro: {e.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
