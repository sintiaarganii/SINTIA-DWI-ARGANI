// SINTIA_DWI_ARGANI/Filters/AuthorizeRoleAttribute.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace SINTIA_DWI_ARGANI.Filters
{
    public class AuthorizeRoleAttribute : ActionFilterAttribute
    {
        private readonly string[] _allowedRoles;

        public AuthorizeRoleAttribute(params string[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var role = context.HttpContext.Session.GetString("Role");

            if (!_allowedRoles.Contains(role))
            {
                context.Result = new RedirectToRouteResult(
                    new { controller = "Authentication", action = "LoginCashier" });
            }

            base.OnActionExecuting(context);
        }
    }
}