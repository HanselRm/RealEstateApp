

using RealStateAppProg3.Core.Application.ViewModels.UpgradeProperty;

namespace RealStateAppProg3.Core.Application.ViewModels.Property
{
    public class SavePropertyViewModel
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
        public List<UpgradePropertyViewModel>? Upgrades { get; set; }
        //id usuario
        public string IdUser { get; set; }
    }
}
