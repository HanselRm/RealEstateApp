

namespace RealStateAppProg3.Core.Domain.Entities
{
    public class PropertyFav
    {
        public string IdUser { get; set; }
        public int IdProperty {  get; set; }

        public Property? Property { get; set; }
    }
}
