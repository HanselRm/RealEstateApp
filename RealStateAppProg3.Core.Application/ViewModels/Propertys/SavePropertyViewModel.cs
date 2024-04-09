

using Microsoft.AspNetCore.Http;
using RealStateAppProg3.Core.Application.ViewModels.UpgradeProperty;
using System.ComponentModel.DataAnnotations;

namespace RealStateAppProg3.Core.Application.ViewModels.Propertys
{
    public class SavePropertyViewModel
    {
        public string Code { get; set; }
        [Required]
        [DataType(DataType.Text,ErrorMessage = "Debe colocar una descripcion")]
        public string Description { get; set; }
        [Range(0,double.MaxValue)]
        [Required(ErrorMessage = "Debe colocar un precio de venta")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "Debe colocar el numero de habitaciones")]
        [Range(0,int.MaxValue)]
        public int NumberRooms { get; set; }

        [Required(ErrorMessage = "Debe colocar el tamaño")]
        [Range(0,double.MaxValue)]
        public int SizeInMeters { get; set; }
        [Required(ErrorMessage = "Debe colocar el numero de baños")]
        [Range(0, int.MaxValue)]
        public int Bathrooms { get; set; }

        [Range(0, int.MaxValue)]
        public int IdTypeProperty { get; set; }
        [Range(0, int.MaxValue)]
        public int IdTypeSale { get; set; }

        public string? UrlImage1 { get; set; }
        public string? UrlImage2 { get; set; }
        public string? UrlImage3 { get; set; }
        public string? UrlImage4 { get; set; }

        [Required(ErrorMessage = "Debe colocar minimo una foto de la propiedad")]
        public IFormFile Img1 {  get; set; }
        public IFormFile? Img2 { get; set; }
        public IFormFile? Img3 { get; set; }
        public IFormFile? Img4 { get; set; }
        public List<int> UpgradesId { get; set; }
        //id usuario
        public string IdUser { get; set; }
    }
}
