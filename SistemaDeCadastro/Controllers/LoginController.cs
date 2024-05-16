using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastro.Models;
using SistemaDeCadastro.Repositorio;

namespace SistemaDeCadastro.Controllers
{
    public class LoginController : Controller
    {

        public readonly IUsuarioRepositorio _usuarioRepositorio;
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
                    UserModel usuario = _usuarioRepositorio.BuscarPorLogin(dadosLogin.Login);
                    if (usuario != null){
                        if (usuario.VerificarSenha(dadosLogin.Senha)){
                            Console.WriteLine("Chegou aqui - achou a senha e o usu");
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemError"] = "Senha incorreta";
                        Console.WriteLine("Chegou aqui - achou o usuario");
                    }
                    TempData["MensagemError"] = "Usuario n�o encontrado";
                    Console.WriteLine("Chegou aqui - n�o achou o usuario");
                }
                return View("Index", dadosLogin);
            }
            catch(System.Exception e)
            {
                TempData["MensagemError"] = $"Ops, n�o conseguimos logar, detalhe do erro: {e.Message}";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}