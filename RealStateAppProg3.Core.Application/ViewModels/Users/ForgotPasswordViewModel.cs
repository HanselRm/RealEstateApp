

using System.ComponentModel.DataAnnotations;

namespace RealStateAppProg3.Core.Application.ViewModels.Users
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Debe colocar el correo o nombre de usuario")]
        public string Email { get; set; }
        public bool? HasError { get; set; }
        public string? Error { get; set; }
    }
}
