
using RealStateAppProg3.Core.Application.ViewModels.PropertyFav;
using RealStateAppProg3.Core.Application.ViewModels.TypeProperty;
using RealStateAppProg3.Core.Application.ViewModels.TypeSale;
using RealStateAppProg3.Core.Domain.Entities;

namespace RealStateAppProg3.Core.Application.ViewModels.Propertys
{
    public class PropertyViewModelDetail
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int NumberRooms { get; set; }
        public int SizeInMeters { get; set; }
        public int Bathrooms { get; set; }
        public string TypeProperty { get; set; }
        public string TypeSale { get; set; }
        public string UrlImage1 { get; set; }
        public string? UrlImage2 { get; set; }
        public string? UrlImage3 { get; set; }
        public string? UrlImage4 { get; set; }
        //id usuario
        public string IdUser { get; set; }
        public List<string> Upgrades { get; set; }
        public string NameAgent { get; set; }
        public string PhoneNumber { get; set; }
        public string fotoAgente { get; set; }
        public string EmailAgente { get; set; }

    }
}
