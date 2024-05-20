using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastro.Models;
using System.Text.Json;

namespace SistemaDeCadastro.Filters
{
    public class PaginaParaUsuarioLogado : ActionFilterAttribute
    {
        //Iremos filtrar a pagina para que somente usuarios logados possam acessar
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            #pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
            string usuario = context.HttpContext.Session.GetString("SessaoUsuarioLogado");
            #pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.

            if (string.IsNullOrEmpty(usuario)) {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "index" }) );
            }
            else {
                UserModel usuarioModel = JsonSerializer.Deserialize<UserModel>(usuario);
                if (usuarioModel == null) {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "index" }) );
                }
            }
            
            base.OnActionExecuting(context);

        }

    }
}