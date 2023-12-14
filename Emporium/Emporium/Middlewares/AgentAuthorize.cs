using Emporium.Controllers;
using EmporiumApp.Core.Application.Dtos.Account;
using EmporiumApp.Core.Application.Enums;
using EmporiumApp.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Emporium.Middlewares
{
    public class AgentAuthorize
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public AgentAuthorize(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = _httpContextAccesor.HttpContext.Session.Get<AuthenticationResponse>("user");
            if (user != null)
            {
                var isAgent = user.Roles.Contains(Roles.Agent.ToString());
                if (!isAgent)
                {
                    var controller = (HomeController)context.Controller;
                    context.Result = controller.RedirectToAction("AccessDenied", "User");
                }
                else await next();
            }
        }
    }
}
