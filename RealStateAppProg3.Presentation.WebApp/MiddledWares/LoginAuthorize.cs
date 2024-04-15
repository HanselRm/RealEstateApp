using Microsoft.AspNetCore.Mvc.Filters;
using RealStateAppProg3.Core.Application.Enums;
using RealStateAppProg3.Presentation.WebApp.Controllers;
using System.Data;

namespace RealStateAppProg3.Presentation.WebApp.MiddledWares
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        private readonly ValidateUser _userSession;
        public LoginAuthorize(ValidateUser userSession)
        {
            _userSession = userSession;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = _userSession.HasUser();
            if (user == null)
            {
                var controller = (UserController)context.Controller;
                if (user.Roles.Contains(RoleENum.Admin.ToString()))
                {
                    context.Result = controller.RedirectToRoute(new { controller = "Admin", action = "Dashboard" });
                }
                else if (user.Roles.Contains(RoleENum.Client.ToString()))
                {
                    context.Result = controller.RedirectToRoute(new { controller = "Client", action = "Home" });
                }
                else if (user.Roles.Contains(RoleENum.Agent.ToString()))
                {
                    context.Result = controller.RedirectToRoute(new { controller = "Agent", action = "Index" });
                }
            }
            else
            {
                await next();
            }
        }
    }
}
