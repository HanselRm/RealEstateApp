﻿using AutoMapper;
using RealStateAppProg3.Core.Application.Dtos.Account;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.Users;


namespace RealStateAppProg3.Core.Application.Services
{
    public class UserService
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
    }
}
