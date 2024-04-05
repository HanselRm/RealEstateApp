
using System.ComponentModel.DataAnnotations;

namespace RealStateAppProg3.Core.Application.ViewModels.Users
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Debes de colocar el correo del usuario. ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debes de tener un token. ")]
        public string Token { get; set; }

        [Required(ErrorMessage = "Debes Ingresar una Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "debes confirmar tu contraseña")]
        [Compare(nameof(Password), ErrorMessage = "Ambas contraseña deben coincidir")]
        public string ConfirmPassword { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
