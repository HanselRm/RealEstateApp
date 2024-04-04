
using RealStateAppProg3.Core.Domain.Entities;

namespace RealStateAppProg3.Core.Application.ViewModels.TypeProperty
{
    public class TypePropertyViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public List<Property>? Properties { get; set; }
    }
}
