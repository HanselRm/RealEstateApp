using AutoMapper;
using RealStateAppProg3.Core.Application.Dtos.Account;
using RealStateAppProg3.Core.Application.ViewModels.TypeProperty;
using RealStateAppProg3.Core.Application.ViewModels.TypeSale;
using RealStateAppProg3.Core.Application.ViewModels.Users;
using RealStateAppProg3.Core.Application.Features.Property.Commands.CreateProperty;
using RealStateAppProg3.Core.Application.Features.Property.Commands.UpdatePropertyByIdCommand;
using RealStateAppProg3.Core.Application.Features.Property.Queries.GetAllProperties;
using RealStateAppProg3.Core.Application.ViewModels.UpgradeProperty;
using RealStateAppProg3.Core.Domain.Entities;
using RealStateAppProg3.Core.Application.ViewModels.Propertys;
using RealStateAppProg3.Core.Application.ViewModels.Upgrades;
using RealStateAppProg3.Core.Application.ViewModels.PropertyFav;

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

            #region Authentication 
            CreateMap<AuthenticationRequest, LoginViewModel>()
               .ForMember(x => x.HasError, opt => opt.Ignore())
               .ForMember(x => x.Error, opt => opt.Ignore())
               .ReverseMap();
            #endregion

            #region ForgotPassword 
            CreateMap<ForgotPassowordRequest, ForgotPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #region resetPassword 
            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #region Property
            CreateMap<Property, PropertyViewModel>()
                .ForMember(x => x.TypeProperty, opt => opt.Ignore())
                .ForMember(x => x.TypeSale, opt => opt.Ignore())
                .ForMember(x => x.Upgrades, opt => opt.Ignore())
                .ForMember(x => x.propertyFavs, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.TypeProperty, opt => opt.Ignore())
                .ForMember(x => x.TypeSale, opt => opt.Ignore())
                .ForMember(x => x.Upgrades, opt => opt.Ignore())
                .ForMember(x => x.propertyFavs, opt => opt.Ignore());

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

            CreateMap<Property, SavePropertyViewModel>()
                .ReverseMap()
                .ForMember(x => x.Upgrades, opt => opt.Ignore())
                .ForMember(x => x.TypeProperty, opt => opt.Ignore())
                .ForMember(x => x.TypeSale, opt => opt.Ignore())
                .ForMember(x => x.propertyFavs, opt => opt.Ignore());

            CreateMap<Property, SavePropertyFavViewModel>()
                .ReverseMap()
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
            #region Upgrade
            CreateMap<Upgrades, UpgradeViewModel>();
            CreateMap<Upgrades, SaveUpgradesViewModel>()
                .ReverseMap()
                .ForMember(x => x.Properties, opt => opt.Ignore());
            #endregion
        }
    }
}
