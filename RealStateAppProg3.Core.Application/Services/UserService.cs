using AutoMapper;
using RealStateAppProg3.Core.Application.Interfaces.Service;


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

    }
}
