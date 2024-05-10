
using InternetBanking.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.UpgradeProperty;
using RealStateAppProg3.Core.Domain.Entities;

namespace RealStateAppProg3.Core.Application.Interfaces.Service
{
    public interface IUpgradePropertyService :
        IBaseService<UpgradePropertyViewModel,SaveUpgradePropertyViewModel,UpgradesProperty>
    {
    }
}
