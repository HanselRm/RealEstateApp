﻿using AutoMapper;
using RealStateAppProg3.Core.Application.ViewModels.TypeProperty;
using RealStateAppProg3.Core.Application.ViewModels.TypeSale;
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
        }
    }
}
