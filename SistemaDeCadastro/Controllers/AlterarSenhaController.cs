using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastro.Helper;
using SistemaDeCadastro.Models;
using SistemaDeCadastro.Repositorio;

namespace SistemaDeCadastro.Controllers{
    public class AlterarSenhaController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public AlterarSenhaController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Alterar(AlterarSenhaModel alterarSenha)
        {
            if (ModelState.IsValid)
            {
                //alterar senha
                var usuario = _sessao.BuscarSessaoDoUsuario();
                if (_usuarioRepositorio.AlterarSenha(usuario, alterarSenha))
                {
                    TempData["MensagemSucesso"] = "Senha alterada com sucesso!";
                    return RedirectToAction("Index", "Home");
                }
                TempData["MensagemErro"] = "Ops, não conseguimos alterar a sua senha, verifique os campos e tente novamente.";
                return View("Index");
            }
            TempData["MensagemErro"] = "Ops, não conseguimos alterar a sua senha, verifique os campos e tente novamente.";
            return View("Index", alterarSenha);
        }
    }
}