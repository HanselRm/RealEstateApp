
using InternetBanking.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.Upgrades;
using RealStateAppProg3.Core.Domain.Entities;

namespace RealStateAppProg3.Core.Application.Interfaces.Service
{
    public interface IUpgradeService : IBaseService<UpgradeViewModel,SaveUpgradesViewModel,Upgrades>
    {
    }
}
