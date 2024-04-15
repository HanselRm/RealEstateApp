
using RealStateAppProg3.Core.Application.ViewModels.Propertys;

namespace RealStateAppProg3.Core.Application.ViewModels.PropertyFav
{
    public class PropertyFavViewModel
    {
        public string IdUser { get; set; }
        public string IdProperty { get; set; }

        public PropertyViewModel? Property { get; set; }
    }
}
