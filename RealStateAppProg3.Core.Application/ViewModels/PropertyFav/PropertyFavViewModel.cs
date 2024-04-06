
using RealStateAppProg3.Core.Application.ViewModels.Property;

namespace RealStateAppProg3.Core.Application.ViewModels.PropertyFav
{
    public class PropertyFavViewModel
    {
        public string IdUser { get; set; }
        public int IdProperty { get; set; }

        public PropertyViewModel? Property { get; set; }
    }
}
