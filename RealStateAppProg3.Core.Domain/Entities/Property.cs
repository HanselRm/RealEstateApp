
using RealStateAppProg3.Core.Domain.Common;

namespace RealStateAppProg3.Core.Domain.Entities
{
    public class Property 
    {
        public string Code {  get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int NumberRooms { get; set; }
        public int SizeInMeters {  get; set; }
        public int Bathrooms { get; set; }
        public int IdTypeProperty { get; set; }
        public int IdTypeSale { get; set; }
        public string UrlImage1 {  get; set; }
        public string? UrlImage2 {  get; set; }
        public string? UrlImage3 {  get; set; }
        public string? UrlImage4 {  get; set; }
        public DateTime? Created { get; set; }
        //id usuario
        public string IdUser { get; set; }
        public TypeProperty TypeProperty { get; set; }
        public TypeSale TypeSale { get; set; }
        public List<UpgradesProperty> Upgrades { get; set; }
        public List<PropertyFav> propertyFavs { get; set; }
    }
}
