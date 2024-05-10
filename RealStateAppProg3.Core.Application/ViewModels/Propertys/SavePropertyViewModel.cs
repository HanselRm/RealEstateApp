using Microsoft.AspNetCore.Http;
using RealStateAppProg3.Core.Application.ViewModels.UpgradeProperty;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealStateAppProg3.Core.Application.ViewModels.Propertys
{
    public class SavePropertyViewModel
    {
        public string Code { get; set; }

        [Required(ErrorMessage = "Debe colocar una descripción")]
        [DataType(DataType.Text, ErrorMessage = "Debe colocar una descripcion")]
        public string Description { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0")]
        [Required(ErrorMessage = "Debe colocar un precio de venta")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "Debe colocar el numero de habitaciones")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de habitaciones debe ser mayor que 0")]
        public int NumberRooms { get; set; }

        [Required(ErrorMessage = "Debe colocar el tamaño")]
        [Range(1, double.MaxValue, ErrorMessage = "El tamaño debe ser mayor que 0")]
        public int SizeInMeters { get; set; }

        [Required(ErrorMessage = "Debe colocar el número de baños")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de baños debe ser mayor que 0")]
        public int Bathrooms { get; set; }

        [Range(1, int.MaxValue)]
        public int IdTypeProperty { get; set; }

        [Range(1, int.MaxValue)]
        public int IdTypeSale { get; set; }

        public string? UrlImage1 { get; set; }
        public string? UrlImage2 { get; set; }
        public string? UrlImage3 { get; set; }
        public string? UrlImage4 { get; set; }

        [Required(ErrorMessage = "Debe colocar mínimo una foto de la propiedad")]
        public IFormFile Img1 { get; set; }
        public IFormFile? Img2 { get; set; }
        public IFormFile? Img3 { get; set; }
        public IFormFile? Img4 { get; set; }
        public DateTime? Created { get; set; }
        public List<int> UpgradesId { get; set; }
        //id usuario
        public string IdUser { get; set; }
    }
}
