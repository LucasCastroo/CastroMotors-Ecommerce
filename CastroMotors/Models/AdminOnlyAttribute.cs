using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CastroMotors.Models
{
    public class AdminOnlyAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Verifica se o usuário está autenticado e se o email é "administrador@gmail.com"
            return httpContext.User.Identity.IsAuthenticated && httpContext.User.Identity.Name == "administrador@gmail.com";
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Redireciona para a tela de acesso negado se o usuário não tiver permissão
            filterContext.Result = new RedirectResult("/Home/AcessoNegado");
        }
    }
}