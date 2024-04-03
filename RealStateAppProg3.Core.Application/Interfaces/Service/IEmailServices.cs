

using RealStateAppProg3.Core.Application.Dtos.Email;

namespace RealStateAppProg3.Core.Application.Interfaces.Service
{
    public interface IEmailServices
    {
        Task sendAsync(EmailRequest request);
    }
}
