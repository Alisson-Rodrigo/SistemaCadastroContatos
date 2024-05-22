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

        public IActionResult Alterar(AlterarSenhaModel alterarSenhaModel)
        {
            try
            {
                UserModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                alterarSenhaModel.Id = usuarioLogado.id;

                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.AlterarSenha(alterarSenhaModel);
                    TempData["MensagemSucesso"] = "Senha alterada com sucesso!";
                    return View("Index", alterarSenhaModel);
                }

                return View("Index", alterarSenhaModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos alterar sua senha, tente novamante, detalhe do erro: {erro.Message}";
                return View("Index", alterarSenhaModel);
            }
        }
    }
}