using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.UI.WPF.Services;
using StoreApp.UI.WPF.Services.ExtraTables;
using StoreApp.UI.WPF.Services.Warehouse;

namespace StoreApp.UI.WPF.Helpers
{
    public class MapServicesStorage
    {
        public SoldProductsMapService SoldProductsMapService { get; set; }
        public WroteOffProductsMapService WroteOffProductsMapService { get; set; }
        public ProductsMapService ProductsMapService { get; set; }
        public CategoriesMapService CategoriesMapService { get; set; }
        public WarehouseSectionsMapService WarehouseSectionsMapService { get; set; }
        public ProvisionersMapService ProvisionersMapService { get; set; }
        public UsersMapService UsersMapService { get; set; }
        public ShopsMapService ShopsMapService { get; set; }



        static MapServicesStorage _instance;
        public static MapServicesStorage Instance => _instance ?? (_instance = new MapServicesStorage());
        private MapServicesStorage()
        {
            SoldProductsMapService = new SoldProductsMapService();
            WroteOffProductsMapService = new WroteOffProductsMapService();
            ProductsMapService = new ProductsMapService();
            CategoriesMapService = new CategoriesMapService();
            WarehouseSectionsMapService = new WarehouseSectionsMapService();
            ProvisionersMapService = new ProvisionersMapService();
            UsersMapService = new UsersMapService();
            ShopsMapService = new ShopsMapService();
        }
    }
}
