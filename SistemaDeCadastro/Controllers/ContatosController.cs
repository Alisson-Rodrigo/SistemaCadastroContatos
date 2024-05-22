using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastro.Filters;
using SistemaDeCadastro.Models;
using SistemaDeCadastro.Repositorio;
using SistemaDeCadastro.Helper;

namespace SistemaDeCadastro.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ContatosController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;
        public ContatosController(IContatoRepositorio contatoRepositorio, ISessao sessao)
        {
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();
            List<ContatoModel> contatos = _contatoRepositorio.GetContatoList(usuario.id);
            return View(contatos);
        }
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(int Id)
        {
            var contato = _contatoRepositorio.InfoContato(Id);
            return View(contato);
        }
        public IActionResult ApagarConfirmacao(int Id)
        {
            var contato = _contatoRepositorio.InfoContato(Id);
            return View(contato);
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            //tentar adicionar o contato
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel user = _sessao.BuscarSessaoDoUsuario();
                    contato.UsuarioID = user.id;

                    _contatoRepositorio.Adicionar(contato);
                    //Armazena uma mensagem na sessão
                    TempData["MensagemSucesso"] = $"Contato adicionado com sucesso.";
                    return RedirectToAction("Index");
                }
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o seu contato.";
                //retorne para a mesma pagina com msg de erro
                return View(contato);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o seu contato, detalhe do erro: {e.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Editar(ContatoModel contato, int Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel user = _sessao.BuscarSessaoDoUsuario();
                    contato.UsuarioID = user.id;

                    _contatoRepositorio.EditarContato(contato, Id);
                    TempData["MensagemSucesso"] = $"Contato editado com sucesso.";
                    return RedirectToAction("index");
                }

                return View(contato);

            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos editar o seu contato, detalhe do erro: {e.Message}";
                return RedirectToAction("Index");
            }

        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _contatoRepositorio.DeletarContato(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = $"Contato deletado com sucesso.";
                    return RedirectToAction("Index");
                }
                TempData["MensagemErro"] = $"Ops, não conseguimos deletar o seu contato.";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos deletar o seu contato, detalhe do erro: {e.Message}";
                return RedirectToAction("Index");
            }

        }

    }
}
