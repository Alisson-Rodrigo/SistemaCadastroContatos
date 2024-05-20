using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastro.Models;
using SistemaDeCadastro.Repositorio;
using SistemaDeCadastro.Helper;

namespace SistemaDeCadastro.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }
        public IActionResult Index()
        {   
            //Se o usuario estiver logado, redireciona para a pagina principal
            if (_sessao.BuscarSessaoDoUsuario() != null)
            {
                return RedirectToAction("Index", "Home");
            }
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
                            _sessao.CriarSessaoDoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    TempData["MensagemErro"] = "Usuário ou senha inválidos";
                }
                return View("Index");
            }
            catch (Exception e)
            {
                TempData["MensagemError"] = $"Ops, não conseguimos logar, detalhe do erro: {e.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
         public IActionResult Register(UserModel user)
        {
            //tentar adicionar o contato
            try
            {
                if (ModelState.IsValid)
                {
                    if(_usuarioRepositorio.Adicionar(user)) {
                        TempData["MensagemSucesso"] = $"Usuario adicionado com sucesso.";
                        return RedirectToAction("Index", "Login");
                    } else {
                        TempData["MensagemErro"] = $"Ops, usuário já cadastrado, tente novamente.";
                        return View("Register");
                    }
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o seu usuário, verifique os campos e tente novamente.";
                    return View("Register");
                }

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o seu usuário infelizmente, detalhe do erro: {e.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Register () {
            return View();
        }


        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index");
        }

    }
}
