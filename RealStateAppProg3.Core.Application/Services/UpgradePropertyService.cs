
using AutoMapper;
using InternetBanking.Core.Application.Services;
using RealStateAppProg3.Core.Application.Interfaces.Repositories;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.UpgradeProperty;
using RealStateAppProg3.Core.Domain.Entities;

namespace RealStateAppProg3.Core.Application.Services
{
    public class UpgradePropertyService : BaseService<UpgradePropertyViewModel, SaveUpgradePropertyViewModel, UpgradesProperty>, IUpgradePropertyService
    {
        private readonly IMapper _mapper;
        private readonly IUpgradesPropertyRepository _upgradePropertyRepository;
        public UpgradePropertyService(IMapper mapper, IUpgradesPropertyRepository upgradesPropertyRepository) 
            :base(upgradesPropertyRepository,mapper)
        {
            _mapper = mapper;
            _upgradePropertyRepository = upgradesPropertyRepository;
        }
    }
}
