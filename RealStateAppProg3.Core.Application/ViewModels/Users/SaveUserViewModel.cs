

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RealStateAppProg3.Core.Application.ViewModels.Users
{
    public class SaveUserViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Debes ingresar tu Nombre")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Debes Ingresar tu Apellido")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Debes Ingresar tu Cedula")]
        public string Identification { get; set; }
        public bool IsActive { get; set; } = false;
        [Required(ErrorMessage = "Debes Ingresar tu Correo")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Debes Ingresar un Usuario")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Debes Ingresar el numero de telefono")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Debe colocar una contraseña")]
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        [Required(ErrorMessage = "Debe colocar una contraseña")]
        public string ConfirmPassword { get; set; }
        public string TypeUser { get; set; }
        public string PhotoProfileUrl { get; set; }
        public IFormFile file { get; set; }
        public bool IsConfirm { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
