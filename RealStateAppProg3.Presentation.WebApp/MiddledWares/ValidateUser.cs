using Microsoft.AspNetCore.Http;
using RealStateAppProg3.Core.Application.Helpers;
using RealStateAppProg3.Core.Application.Dtos.Account;

namespace RealStateAppProg3.Presentation.WebApp.MiddledWares
{
    public class ValidateUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public AuthenticationResponse HasUser()
        {
            AuthenticationResponse userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

            if (userViewModel == null)
            {
                return null;
            }
            return userViewModel;
        }
    }
}
