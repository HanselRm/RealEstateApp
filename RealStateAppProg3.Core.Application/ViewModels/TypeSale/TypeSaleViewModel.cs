

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RealStateAppProg3.Core.Application.ViewModels.TypeSale
{
    public class TypeSaleViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public List<Property>? Properties { get; set; }
    }
}
