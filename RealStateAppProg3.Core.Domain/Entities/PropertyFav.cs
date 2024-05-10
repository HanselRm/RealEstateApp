

using RealStateAppProg3.Core.Domain.Common;

namespace RealStateAppProg3.Core.Domain.Entities
{
    public class PropertyFav : BaseEntity
    {
        public string IdUser { get; set; }
        public string IdProperty {  get; set; }

        public Property? Property { get; set; }
    }
}
