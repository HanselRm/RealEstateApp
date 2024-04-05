

using System.ComponentModel.DataAnnotations;

namespace RealStateAppProg3.Core.Application.ViewModels.Users
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Debe colocar el correo o nombre de usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Debe colocar una contraseña")]
        public string Password { get; set; }
        public bool? HasError { get; set; }
        public string? Error { get; set; }
    }
}
