using Emporium.Controllers;
using EmporiumApp.Core.Application.Dtos.Account;
using EmporiumApp.Core.Application.Enums;
using EmporiumApp.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Emporium.Middlewares
{
    public class AdminAuthorize
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public AdminAuthorize(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = _httpContextAccesor.HttpContext.Session.Get<AuthenticationResponse>("user");
            if (user != null)
            {
                var isAdmin = user.Roles.Contains(Roles.Admin.ToString());
                if (!isAdmin)
                {
                    var controller = (HomeController)context.Controller;
                    context.Result = controller.RedirectToAction("AccessDenied", "User");
                }
                else await next();
            }
        }
    }
}
