using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastro.Models;
using SistemaDeCadastro.Repositorio;
using SistemaDeCadastro.Helper;


namespace SistemaDeCadastro.Controllersw
{

    public class LoginController : Controller
    {
        private readonly IEnviar _enviar;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEnviar enviar)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _enviar = enviar;
        }
        public IActionResult Index()
        {
            if(_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel dadosLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = _usuarioRepositorio.BuscarPorLogin(dadosLogin.Login);
                    if (usuario != null) {
                        if (usuario.VerificarSenha(dadosLogin.Senha)) {
                            _sessao.CriarSessaoDoUsuario(usuario);
                            TempData["MensagemSucesso"] = $"Seja bem vindo {usuario.name}!";
                            return RedirectToAction("Index", "Home");
                        } 
                    }
                }
                TempData["MensagemErro"] = $"Usuário e/ou senha inválido(s). Por favor, tente novamente.";
                return View("Index");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos logar, detalhe do erro: {e.Message}";
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

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnviarLinkDeRedefinicao(RedefinirSenhaModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = _usuarioRepositorio.BuscarPorEmail(model.Email);
                    if (usuario != null)
                    {
                        //enviar e-mail
                        
                        string novaSenha = usuario.GerarNovaSenha();
                        string mensagem = $"Olá, {usuario.name} sua nova senha é: {novaSenha}";
                        bool emailEnviado = _enviar.EnviarEmail(model.Email, "Redefinição de senha", mensagem);
                        if (emailEnviado)
                        {
                            TempData["MensagemSucesso"] = $"E-mail de redefinição de senha enviado com sucesso.";
                            _usuarioRepositorio.EditarUsuario(usuario, usuario.id);
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Ops, não conseguimos enviar o e-mail de redefinição de senha.";
                        }
                        
                        return RedirectToAction("Index");
                    }
                    TempData["MensagemErro"] = $"E-mail não encontrado.";
                    return View("RedefinirSenha");
                }
                TempData["MensagemErro"] = $"E-mail inválido.";
                return View("RedefinirSenha");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos enviar o e-mail de redefinição de senha, detalhe do erro: {e.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
