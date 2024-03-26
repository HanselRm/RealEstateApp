
using RealStateAppProg3.Core.Domain.Common;

namespace RealStateAppProg3.Core.Domain.Entities
{
    public class UpgradesProperty : BaseEntity
    {
        public int IdProperty { get; set; }
        public int IdUpgrade { get; set; }
        public Property? Property { get; set; }
        public Upgrades? Upgrades { get; set; }

    }
}
