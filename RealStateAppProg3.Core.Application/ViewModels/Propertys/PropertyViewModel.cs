
using RealStateAppProg3.Core.Application.ViewModels.PropertyFav;
using RealStateAppProg3.Core.Application.ViewModels.TypeProperty;
using RealStateAppProg3.Core.Application.ViewModels.TypeSale;
using RealStateAppProg3.Core.Domain.Entities;

namespace RealStateAppProg3.Core.Application.ViewModels.Propertys
{
    public class PropertyViewModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int NumberRooms { get; set; }
        public int SizeInMeters { get; set; }
        public int Bathrooms { get; set; }
        public int IdTypeProperty { get; set; }
        public int IdTypeSale { get; set; }
        public string UrlImage1 { get; set; }
        public string? UrlImage2 { get; set; }
        public string? UrlImage3 { get; set; }
        public string? UrlImage4 { get; set; }
        //id usuario
        public string IdUser { get; set; }
        public TypePropertyViewModel? TypeProperty { get; set; }
        public TypeSaleViewModel? TypeSale { get; set; }
        public List<UpgradesProperty>? Upgrades { get; set; }
        public List<PropertyFavViewModel>? propertyFavs { get; set; }

    }
}
