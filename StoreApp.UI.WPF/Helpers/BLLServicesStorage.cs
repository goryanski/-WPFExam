using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.BLL.Services;
using StoreApp.BLL.Services.ExtraTables;
using StoreApp.BLL.Services.Warehouse;
using StoreApp.DAL.Interfaces;
using StoreApp.DAL.Repositories;

namespace StoreApp.UI.WPF.Helpers
{
    public class BLLServicesStorage
    {
        IUnitOfWork uow;


        public SoldProductsService SoldProductsService { get; set; }
        public WroteOffProductsService WroteOffProductsService { get; set; }
        public ProductsService ProductsService { get; set; }
        public CategoriesService CategoriesService { get; set; }
        public WarehouseSectionsService WarehouseSectionsService { get; set; }
        public ProvisionersService ProvisionersService { get; set; }
        public UsersService UsersService { get; set; }
        public ShopsService ShopsService { get; set; }
      


        static BLLServicesStorage _instance;
        public static BLLServicesStorage Instance => _instance ?? (_instance = new BLLServicesStorage());
        private BLLServicesStorage()
        {          
            uow = new UnitOfWork(@"Data Source=DESKTOP-RG3HTFI;Initial Catalog=ProductsWarehouse;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            WroteOffProductsService = new WroteOffProductsService(uow);
            SoldProductsService = new SoldProductsService(uow);
            ProductsService = new ProductsService(uow);
            CategoriesService = new CategoriesService(uow);
            WarehouseSectionsService = new WarehouseSectionsService(uow);
            ProvisionersService = new ProvisionersService(uow);
            UsersService = new UsersService(uow);
            ShopsService = new ShopsService(uow);
        }
    }
}
