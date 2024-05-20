using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SistemaDeCadastro.Models;

namespace ControleDeContatos.Filters
{
    public class PaginaParaUsuarioLogado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            #pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
            string sessaoUsuario = context.HttpContext.Session.GetString("SessaoUsuarioLogado");
            #pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.

            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "index" } });
            }
            else
            {
                #pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
                UserModel usuario = JsonSerializer.Deserialize<UserModel>(sessaoUsuario);
                #pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.

                if(usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "index" } });
                }
            }

            base.OnActionExecuting(context);
        }
    }
}