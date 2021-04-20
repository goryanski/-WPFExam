using AutoMapper;
using StoreApp.BLL.DTO;
using StoreApp.BLL.DTO.ExtraTables;
using StoreApp.BLL.DTO.Warehouse;
using StoreApp.DAL.Entities;
using StoreApp.DAL.Entities.ExtraTables;
using StoreApp.DAL.Entities.Warehouse;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.BLL.Automapper.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();

            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();

            CreateMap<WarehouseSection, WarehouseSectionDTO>();
            CreateMap<WarehouseSectionDTO, WarehouseSection>();

            CreateMap<SoldProduct, SoldProductDTO>();
            CreateMap<SoldProductDTO, SoldProduct>();

            CreateMap<WroteOffProduct, WroteOffProductDTO>();
            CreateMap<WroteOffProductDTO, WroteOffProduct>();

            CreateMap<Provisioner, ProvisionerDTO>();
            CreateMap<ProvisionerDTO, Provisioner>();

            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<Shop, ShopDTO>();
            CreateMap<ShopDTO, Shop>();

            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>();
        }
    }
}
