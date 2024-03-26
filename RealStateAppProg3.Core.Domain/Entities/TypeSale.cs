
using RealStateAppProg3.Core.Domain.Common;

namespace RealStateAppProg3.Core.Domain.Entities
{
    public class TypeSale : CommonNameDescription
    {
        public List<Property>? Properties { get; set; }

    }
}
