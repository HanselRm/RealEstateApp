

namespace RealStateAppProg3.Core.Application.Features.Property.Queries.GetAllProperties
{
    public class GetAllPropertiesParameters
    {
        public int? IdTypeProperty { get; set; }
        public decimal? MinPrice {  get; set; }
        public decimal? MaxPrice { get; set; }
        public int? NumberRooms { get; set; }
        public int? Bathrooms {  get; set; }
    }
}
