using AutoMapper;
using RealStateAppProg3.Core.Application.Features.Property.Commands.CreateProperty;
using RealStateAppProg3.Core.Application.Features.Property.Commands.UpdatePropertyByIdCommand;
using RealStateAppProg3.Core.Application.Features.Property.Queries.GetAllProperties;
using RealStateAppProg3.Core.Application.ViewModels.Property;
using RealStateAppProg3.Core.Application.ViewModels.TypeProperty;
using RealStateAppProg3.Core.Application.ViewModels.TypeSale;
using RealStateAppProg3.Core.Application.ViewModels.UpgradeProperty;
using RealStateAppProg3.Core.Domain.Entities;

namespace RealStateAppProg3.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Type Sale
            CreateMap<TypeSale, SaveTypeSaleViewModel>()
                .ReverseMap()
                .ForMember(x => x.Properties, opt => opt.Ignore());

            CreateMap<TypeSale, TypeSaleViewModel>();
            #endregion
            #region Type Property
            CreateMap<TypeProperty, SaveTypePropertyViewModel>()
                .ReverseMap()
                .ForMember(x => x.Properties, opt => opt.Ignore());

            CreateMap<TypeProperty, TypePropertyViewModel>();
            #endregion
            #region Property
            CreateMap<Property, PropertyViewModel>();
            CreateMap<Property, CreatePropertyCommand>()
                .ForMember(x => x.Upgrades, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Upgrades, opt => opt.Ignore())
                .ForMember(x => x.TypeProperty, opt => opt.Ignore())
                .ForMember(x => x.TypeSale, opt => opt.Ignore())
                .ForMember(x => x.propertyFavs, opt => opt.Ignore());

            CreateMap<Property, UpdatePropertyByIdCommand>()
                .ReverseMap()
                .ForMember(x => x.Upgrades, opt => opt.Ignore())
                .ForMember(x => x.TypeProperty, opt => opt.Ignore())
                .ForMember(x => x.TypeSale, opt => opt.Ignore())
                .ForMember(x => x.propertyFavs, opt => opt.Ignore());

            CreateMap<Property, UpdatePropertyResponse>()
                .ReverseMap()
                .ForMember(x => x.Upgrades, opt => opt.Ignore())
                .ForMember(x => x.TypeProperty, opt => opt.Ignore())
                .ForMember(x => x.TypeSale, opt => opt.Ignore())
                .ForMember(x => x.propertyFavs, opt => opt.Ignore());

            CreateMap<Property,SavePropertyViewModel>()
                .ForMember(x => x.Upgrades, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Upgrades, opt => opt.Ignore())
                .ForMember(x => x.TypeProperty, opt => opt.Ignore())
                .ForMember(x => x.TypeSale, opt => opt.Ignore())
                .ForMember(x => x.propertyFavs, opt => opt.Ignore());

            CreateMap<GetAllPropertiesParameters, GetAllPropertiesQuery>();
            #endregion
            #region UpgradeProperty
            CreateMap<UpgradesProperty, SaveUpgradePropertyViewModel>()
                .ReverseMap()
                .ForMember(x => x.Property, opt => opt.Ignore())
                .ForMember(x => x.Upgrades, opt => opt.Ignore());
            CreateMap<UpgradesProperty, UpgradePropertyViewModel>();
            #endregion
        }
    }
}
