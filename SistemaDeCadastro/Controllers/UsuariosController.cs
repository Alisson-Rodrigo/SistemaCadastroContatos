using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastro.Repositorio;
using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuariosController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        public IActionResult Index()
        {
            var usuarios = _usuarioRepositorio.GetUserList();
            return View(usuarios);
        }

        public IActionResult Register () {
            return View();
        }

        public IActionResult Editar(int Id)
        {
            var usuario = _usuarioRepositorio.InfoUsuario(Id);
            return View(usuario);
        }


        [HttpPost]
        public IActionResult Editar(UserModel usuario, int Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.EditarUsuario(usuario, Id);
                    TempData["MensagemSucesso"] = $"Usuario editado com sucesso.";
                    return RedirectToAction("index");
                }

                return View(usuario);

            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos editar o seu usuario, detalhe do erro: {e.Message}";
                return RedirectToAction("Index");
            }

        }

        public IActionResult ApagarConfirmacao(int Id)
        {
            var usuario = _usuarioRepositorio.InfoUsuario(Id);
            return View(usuario);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.DeletarUsuario(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = $"Usuário deletado com sucesso.";
                    return RedirectToAction("Index");
                }
                TempData["MensagemErro"] = $"Ops, não conseguimos deletar o seu usuário.";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos deletar o seu usuário, detalhe do erro: {e.Message}";
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
                    } else {
                        TempData["MensagemErro"] = $"Ops, usuário já cadastrado, tente novamente.";
                    }
                }
                return RedirectToAction("index", "Login");

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o seu usuário, detalhe do erro: {e.Message}";
                return RedirectToAction("Index");
            }
        }


    }
}
