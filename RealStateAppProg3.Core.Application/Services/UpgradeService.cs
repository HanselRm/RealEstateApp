
using AutoMapper;
using InternetBanking.Core.Application.Services;
using RealStateAppProg3.Core.Application.Interfaces.Repositories;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.Upgrades;
using RealStateAppProg3.Core.Domain.Entities;

namespace RealStateAppProg3.Core.Application.Services
{
    public class UpgradeService : BaseService<UpgradeViewModel, SaveUpgradesViewModel, Upgrades>, IUpgradeService
    {
        private readonly IUpgradeRepository _upgradeRepsitory;
        private readonly IMapper _mapper;
        public UpgradeService(IUpgradeRepository upgradeRepository, IMapper mapper) : base(upgradeRepository, mapper)
        {
            _upgradeRepsitory = upgradeRepository;
            _mapper = mapper;
        }
    }
}
