

using RealStateAppProg3.Core.Application.Dtos.Account;
using RealStateAppProg3.Core.Application.ViewModels.Users;

namespace RealStateAppProg3.Core.Application.Interfaces.Service
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccount(string userId, string token);
        Task<ForgotPassWordResponse> ForgotPasswordRequestAsync(ForgotPassowordRequest request, string origin);
        Task<SaveUserViewModel> RegisterAsync(SaveUserViewModel vm, string origin);
        Task<SaveUserViewModel> UpdateAsync(SaveUserViewModel vm);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task SignOutAsync();
    }
}
