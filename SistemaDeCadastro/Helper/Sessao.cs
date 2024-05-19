using SistemaDeCadastro.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace SistemaDeCadastro.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext;
        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public UserModel BuscarSessaoDoUsuario()
        {
            string usuarioJon = _httpContext.HttpContext.Session.GetString("SessaoUsuarioLogado");
            if (string.IsNullOrEmpty(usuarioJon)) return null;
            UserModel usuario = JsonSerializer.Deserialize<UserModel>(usuarioJon);
            return usuario;
        }

        public void CriarSessaoDoUsuario(UserModel usuario)
        {
            string usuarioJson = JsonSerializer.Serialize(usuario);
            #pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
            _httpContext.HttpContext.Session.SetString("SessaoUsuarioLogado", usuarioJson);
            #pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.
        }

        public void RemoverSessaoDoUsuario()
        {

            #pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
            _httpContext.HttpContext.Session.Remove("SessaoUsuarioLogado");
            #pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.
        }
    }
}