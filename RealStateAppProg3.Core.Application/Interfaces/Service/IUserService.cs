using RealStateAppProg3.Core.Application.Dtos.Account;
using RealStateAppProg3.Core.Application.ViewModels.Users;

namespace RealStateAppProg3.Core.Application.Interfaces.Service
{
    public interface IUserService
    {
        Task<string> confirmEmailAsync(string userId, string token);
        Task<ForgotPassWordResponse> ForgotPassWordAsync(ForgotPasswordViewModel vm, string origin);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<SaveUserViewModel> RegisterAsync(SaveUserViewModel vm, string origin);
        Task<SaveUserViewModel> UpdateAsync(SaveUserViewModel vm);
        //obtener los usuarios con el rol administrador
        Task<List<SaveUserViewModel>> GetUsersAdmin();
        //obtener usuarios por role
        Task<List<SaveUserViewModel>> GetUsersByRole(string role);
        Task<SaveUserViewModel> GetByIdWithoutRol(string id);
        Task<ResetPasswordResponse> ResetPassWordAsync(ResetPasswordViewModel vm);
        Task SignOutAsync();
        Task<List<SaveUserViewModel>> GetAllAsync();
    }
}