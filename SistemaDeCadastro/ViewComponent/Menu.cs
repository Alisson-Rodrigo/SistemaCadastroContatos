using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastro.Models;
using System.Text.Json;
using System.Threading.Tasks;


namespace SistemaDeCadastro.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string SessaoUsuario = HttpContext.Session.GetString("SessaoUsuarioLogado");
            if (string.IsNullOrEmpty(SessaoUsuario)) return View("MenuSemUsuario");

            UserModel usuario = JsonSerializer.Deserialize<UserModel>(SessaoUsuario);
            return View(usuario);
        }
    }
}