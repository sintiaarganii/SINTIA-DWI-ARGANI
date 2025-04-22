using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

public class BaseController : Controller
{
    protected bool IsUserLoggedIn()
    {
        return HttpContext.Session.GetString("IsLoggedIn") == "true";
    }

    protected string GetUserRole()
    {
        return HttpContext.Session.GetString("Role");
    }

    protected int GetUserId()
    {
        return HttpContext.Session.GetInt32("UserId") ?? 0;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!IsUserLoggedIn() && !AllowAnonymous())
        {
            context.Result = RedirectToAction("LoginCashier", "Authentication");
        }

        base.OnActionExecuting(context);
    }

    private bool AllowAnonymous()
    {
        return ControllerContext.ActionDescriptor.EndpointMetadata
            .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));
    }
}