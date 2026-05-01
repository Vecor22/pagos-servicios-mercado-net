using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mercado.PL.GUI.Filters
{
    public class RolAutorizadoAttribute : ActionFilterAttribute
    {
        private readonly string[] rolesPermitidos;

        public RolAutorizadoAttribute(params string[] roles)
        {
            rolesPermitidos = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var rol = context.HttpContext.Session.GetString("Rol");

            if (string.IsNullOrEmpty(rol) || !rolesPermitidos.Contains(rol))
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
        }
    }
}