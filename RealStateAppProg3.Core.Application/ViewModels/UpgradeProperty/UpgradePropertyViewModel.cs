
using RealStateAppProg3.Core.Application.ViewModels.Propertys;
using RealStateAppProg3.Core.Application.ViewModels.Upgrades;

namespace RealStateAppProg3.Core.Application.ViewModels.UpgradeProperty
{
    public class UpgradePropertyViewModel
    {
        public int IdProperty { get; set; }
        public int IdUpgrade { get; set; }
        public PropertyViewModel? Property { get; set; }
        public UpgradeViewModel? Upgrades { get; set; }
    }
}
