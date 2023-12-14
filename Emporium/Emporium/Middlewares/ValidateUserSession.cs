using EmporiumApp.Core.Application.Dtos.Account;
using EmporiumApp.Core.Application.Helpers;

namespace Emporium.Middlewares
{
    public class ValidateUserSession
    {
        public readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            AuthenticationResponse userVm = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            bool validate = userVm == null ? false : true;
            return validate;
        }
    }
}
