

using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RealStateAppProg3.Core.Application.ViewModels.Users
{
    public class SaveUserViewModel
    {
        [SwaggerParameter("Id del usuario")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Debes ingresar tu Nombre")]
        [SwaggerParameter("Nombre de la persona")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debes Ingresar tu Apellido")]
        [SwaggerParameter("Apellido de la persona")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debes Ingresar tu Cedula")]
        [SwaggerParameter("Cedula")]
        public string Identification { get; set; }

        [JsonIgnore]
        public bool IsActive { get; set; } = false;

        [Required(ErrorMessage = "Debes Ingresar el numero de telefono")]
        [SwaggerParameter("Numero de telefono")]
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public string? PhotoProfileUrl { get; set; }


        [Required(ErrorMessage = "Debes Ingresar la foto del user")]
        [SwaggerParameter("Foto de perfil")]
        public IFormFile file { get; set; }

        [Required(ErrorMessage = "Debes Ingresar un Usuario")]
        [SwaggerParameter("Nombre de usuario")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Debes Ingresar tu Correo")]
        [SwaggerParameter("Correo electronico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [SwaggerParameter("Contraseña")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        [Required(ErrorMessage = "Debe colocar una contraseña")]
        public string ConfirmPassword { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Debe colocar una el tipo de usuario")]
        public string TypeUser { get; set; }

        [JsonIgnore]
        public bool IsConfirm { get; set; }
        [JsonIgnore]
        public bool? HasError { get; set; }
        [JsonIgnore]
        public string? Error { get; set; }
    }
}
