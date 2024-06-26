﻿using AutoMapper;
using RealStateAppProg3.Core.Application.Dtos.Account;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.Users;

namespace RealStateAppProg3.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountServices;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountServices, IMapper mapper)
        {
            _accountServices = accountServices;
            _mapper = mapper;
        }

        //metodo de login
        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse userResponse = await _accountServices.AuthenticateAsync(loginRequest);

            return userResponse;
        }

        //metodo de cerrar sesion
        public async Task SignOutAsync()
        {
            await _accountServices.SignOutAsync();
        }

        //metodo de create
        public async Task<SaveUserViewModel> RegisterAsync(SaveUserViewModel vm, string origin)
        {
            return await _accountServices.RegisterAsync(vm, origin);
        }
        //metodo de actualizar 
        public async Task<SaveUserViewModel> UpdateAsync(SaveUserViewModel vm)
        {
            return await _accountServices.UpdateAsync(vm);
        }

        //metodo de obtener user sin rol
        public async Task<SaveUserViewModel> GetByIdWithoutRol(string id)
        {
            return await _accountServices.GetByIdWithoutRol(id);
        }

        //metodo para confirmar email
        public async Task<string> confirmEmailAsync(string userId, string token)
        {
            return await _accountServices.ConfirmAccount(userId, token);
        }

        //metodo para olvidar contraseña
        public async Task<ForgotPassWordResponse> ForgotPassWordAsync(ForgotPasswordViewModel vm, string origin)
        {
            ForgotPassowordRequest forgotPass = _mapper.Map<ForgotPassowordRequest>(vm);
            return await _accountServices.ForgotPasswordRequestAsync(forgotPass, origin);
        }

        //metodo para resetear el password
        public async Task<ResetPasswordResponse> ResetPassWordAsync(ResetPasswordViewModel vm)
        {
            ResetPasswordRequest resetPass = _mapper.Map<ResetPasswordRequest>(vm);
            return await _accountServices.ResetPasswordAsync(resetPass);
        }

        public async Task<List<SaveUserViewModel>> GetUsersAdmin()
        {
            var users = await _accountServices.GetUsersAdmin();
            return users;
        }

        public async Task<List<SaveUserViewModel>> GetAllAsync()
        {
            return await _accountServices.GetAllAsync();
        }
        //obtener usuarios por rol
        public async Task<List<SaveUserViewModel>> GetUsersByRole(string role)
        {
            return await _accountServices.GetUsersByRole(role);
        }

        public async Task DeleteByIdAsync(string Code)
        {
            await _accountServices.DeleteUserByIdAsync(Code);
        }
    }
}
