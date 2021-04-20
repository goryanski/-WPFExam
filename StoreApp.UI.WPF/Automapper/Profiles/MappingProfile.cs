using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using StoreApp.BLL.DTO;
using StoreApp.BLL.DTO.ExtraTables;
using StoreApp.BLL.DTO.Warehouse;
using StoreApp.UI.WPF.Models;
using StoreApp.UI.WPF.Models.ExtraModels;
using StoreApp.UI.WPF.Models.Warehouse;

namespace StoreApp.UI.WPF.Automapper.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductUI, ProductDTO>();
            CreateMap<ProductDTO, ProductUI>();

            CreateMap<CategoryUI, CategoryDTO>();
            CreateMap<CategoryDTO, CategoryUI>();

            CreateMap<WarehouseSectionUI, WarehouseSectionDTO>();
            CreateMap<WarehouseSectionDTO, WarehouseSectionUI>();

            CreateMap<SoldProductUI, SoldProductDTO>();
            CreateMap<SoldProductDTO, SoldProductUI>();

            CreateMap<WroteOffProductUI, WroteOffProductDTO>();
            CreateMap<WroteOffProductDTO, WroteOffProductUI>();

            CreateMap<ProvisionerUI, ProvisionerDTO>();
            CreateMap<ProvisionerDTO, ProvisionerUI>();

            CreateMap<UserUI, UserDTO>();
            CreateMap<UserDTO, UserUI>();

            CreateMap<ShopUI, ShopDTO>();
            CreateMap<ShopDTO, ShopUI>();

            CreateMap<OrderUI, OrderDTO>();
            CreateMap<OrderDTO, OrderUI>();
        }
    }
}
