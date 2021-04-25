using StoreApp.DAL.EF;
using StoreApp.DAL.Entities;
using StoreApp.DAL.Entities.ExtraTables;
using StoreApp.DAL.Entities.Warehouse;
using StoreApp.DAL.Interfaces;
using StoreApp.DAL.Repositories.ExtraTables;
using StoreApp.DAL.Repositories.Warehouse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private StoreContext db;

        #region Repositories
        private SoldProductsRepository _soldProductsRepository;
        public SoldProductsRepository SoldProductsRepository =>
           _soldProductsRepository ?? (_soldProductsRepository = new SoldProductsRepository(db));


        private WroteOffProductsRepository _wroteOffProductsRepository;
        public WroteOffProductsRepository WroteOffProductsRepository =>
           _wroteOffProductsRepository ?? (_wroteOffProductsRepository = new WroteOffProductsRepository(db));


        private CategoriesRepository _categoriesRepository;
        public CategoriesRepository CategoriesRepository =>
           _categoriesRepository ?? (_categoriesRepository = new CategoriesRepository(db));



        private ProductsRepository _productsRepository;
        public ProductsRepository ProductsRepository =>
           _productsRepository ?? (_productsRepository = new ProductsRepository(db));


        private WarehouseSectionsRepository _warehouseSectionsRepository;
        public WarehouseSectionsRepository WarehouseSectionsRepository =>
           _warehouseSectionsRepository ?? (_warehouseSectionsRepository = new WarehouseSectionsRepository(db));


        private ProvisionersRepository _provisionersRepository;
        public ProvisionersRepository ProvisionersRepository =>
           _provisionersRepository ?? (_provisionersRepository = new ProvisionersRepository(db));


        private UsersRepository _usersRepository;
        public UsersRepository UsersRepository =>
           _usersRepository ?? (_usersRepository = new UsersRepository(db));


        private ShopsRepository _shopsRepository;
        public ShopsRepository ShopsRepository =>
           _shopsRepository ?? (_shopsRepository = new ShopsRepository(db));


        private OrderRepository _orderRepository;
        public OrderRepository OrderRepository =>
           _orderRepository ?? (_orderRepository = new OrderRepository(db));
        #endregion


        public UnitOfWork(string connectionString)
        {
            db = new StoreContext(connectionString);

            // we don't have to init db async, because we only need to do it once
            //DbInit();
        }

        private void DbInit()
        {
            #region Categories
            Category category1 = new Category { Name = "Fruits" };
            Category category2 = new Category { Name = "Vegetables" };
            Category category3 = new Category { Name = "Juice" };
            Category category4 = new Category { Name = "Cheese" };
            Category category5 = new Category { Name = "Meat" };
            Category category6 = new Category { Name = "Wine" };
            Category category7 = new Category { Name = "Fish" };
            Category category8 = new Category { Name = "Сereals" };
            Category category9 = new Category { Name = "Pasta" };
            Category category10 = new Category { Name = "Sauces" };
            Category category11 = new Category { Name = "Spice" };

            db.Categories.Add(category1);
            db.Categories.Add(category2);
            db.Categories.Add(category3);
            db.Categories.Add(category4);
            db.Categories.Add(category5);
            db.Categories.Add(category6);
            db.Categories.Add(category7);
            db.Categories.Add(category8);
            db.Categories.Add(category9);
            db.Categories.Add(category10);
            db.Categories.Add(category11);
            #endregion

            #region Provisioners
            Provisioner provisioner1 = new Provisioner
            {
                Name = "May Ukraine",
                Mail = "igorok208@gmail.com"
            };
            Provisioner provisioner2 = new Provisioner
            {
                Name = "Regno",
                Mail = "mytestpost201@gmail.com"
            };

            db.Provisioners.Add(provisioner1);
            db.Provisioners.Add(provisioner2);
            #endregion

            #region Warehouse Sections
            WarehouseSection section1 = new WarehouseSection { SectionNumber = 1 };
            WarehouseSection section2 = new WarehouseSection { SectionNumber = 2 };
            WarehouseSection section3 = new WarehouseSection { SectionNumber = 3 };
            WarehouseSection section4 = new WarehouseSection { SectionNumber = 4 };
            WarehouseSection section5 = new WarehouseSection { SectionNumber = 5 };
            WarehouseSection section6 = new WarehouseSection { SectionNumber = 6 };
            WarehouseSection section7 = new WarehouseSection { SectionNumber = 7 };
            WarehouseSection section8 = new WarehouseSection { SectionNumber = 8 };
            WarehouseSection section9 = new WarehouseSection { SectionNumber = 9 };
            WarehouseSection section10 = new WarehouseSection { SectionNumber = 10 };
            WarehouseSection section11 = new WarehouseSection { SectionNumber = 11 };
            WarehouseSection section12 = new WarehouseSection { SectionNumber = 12 };
            WarehouseSection section13 = new WarehouseSection { SectionNumber = 13 };
            WarehouseSection section14 = new WarehouseSection { SectionNumber = 14 };
            WarehouseSection section15 = new WarehouseSection { SectionNumber = 15 };
            WarehouseSection section16 = new WarehouseSection { SectionNumber = 16 };
            WarehouseSection section17 = new WarehouseSection { SectionNumber = 17 };
            WarehouseSection section18 = new WarehouseSection { SectionNumber = 18 };
            WarehouseSection section19 = new WarehouseSection { SectionNumber = 19 };
            WarehouseSection section20 = new WarehouseSection { SectionNumber = 20 };

            db.WarehouseSections.Add(section1);
            db.WarehouseSections.Add(section2);
            db.WarehouseSections.Add(section3);
            db.WarehouseSections.Add(section4);
            db.WarehouseSections.Add(section5);
            db.WarehouseSections.Add(section6);
            db.WarehouseSections.Add(section7);
            db.WarehouseSections.Add(section8);
            db.WarehouseSections.Add(section9);
            db.WarehouseSections.Add(section10);
            db.WarehouseSections.Add(section11);
            db.WarehouseSections.Add(section12);
            db.WarehouseSections.Add(section13);
            db.WarehouseSections.Add(section14);
            db.WarehouseSections.Add(section15);
            db.WarehouseSections.Add(section16);
            db.WarehouseSections.Add(section17);
            db.WarehouseSections.Add(section18);
            db.WarehouseSections.Add(section19);
            db.WarehouseSections.Add(section20);

            db.SaveChanges();
            #endregion

            #region Products  

            #region Fruits - category1
            Product product1 = new Product
            {
                Name = "Banana",
                Weight = 1,
                PrimeCost = 15,
                Price = 33.80m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-3),
                SellBy = DateTime.Now.AddDays(+5),
                AmountInStorage = 10000,
                PhotoPath = "https://img2.zakaz.ua/auchan.1570441954.ad72436478c_2019-10-08_Julia/auchan.1570441954.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category1.Id,
                ProvisionerId = provisioner1.Id,
                SectionId = section1.Id
            };
            Product product2 = new Product
            {
                Name = "Lemon",
                Weight = 1,
                PrimeCost = 15.60m,
                Price = 34.90m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-3),
                SellBy = DateTime.Now.AddDays(+5),
                AmountInStorage = 10000,
                PhotoPath = "https://img3.zakaz.ua/src.1614860713.ad72436478c_2021-03-04_Victor/src.1614860713.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category1.Id,
                ProvisionerId = provisioner1.Id,
                SectionId = section1.Id
            };
            Product product3 = new Product
            {
                Name = "Kiwi Butterfly",
                Weight = 1,
                PrimeCost = 30.60m,
                Price = 59.50m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-3),
                SellBy = DateTime.Now.AddDays(-1),
                AmountInStorage = 10000,
                PhotoPath = "https://img2.zakaz.ua/src.1614861560.ad72436478c_2021-03-04_Victor/src.1614861560.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.1350nowm.jpg.1350x.jpg",
                CategoryId = category1.Id,
                ProvisionerId = provisioner1.Id,
                SectionId = section1.Id
            };
            Product product4 = new Product
            {
                Name = "Apple Mutsu",
                Weight = 1,
                PrimeCost = 12.60m,
                Price = 30.80m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-3),
                SellBy = DateTime.Now.AddDays(+5),
                AmountInStorage = 10000,
                PhotoPath = "https://img2.zakaz.ua/030321.1615458803.ad72436478c_2021-03-11_Svetlana/030321.1615458803.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.350nowm.jpg.350x.jpg",
                CategoryId = category1.Id,
                ProvisionerId = provisioner1.Id,
                SectionId = section1.Id
            };
            Product product5 = new Product
            {
                Name = "Grapes Red globe",
                Weight = 1,
                PrimeCost = 60.60m,
                Price = 129.80m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-3),
                SellBy = DateTime.Now.AddDays(+5),
                AmountInStorage = 10000,
                PhotoPath = "https://img3.zakaz.ua/src.1605020637.ad72436478c_2020-11-10_Victor/src.1605020637.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category1.Id,
                ProvisionerId = provisioner1.Id,
                SectionId = section1.Id
            };

            db.Products.Add(product1);
            db.Products.Add(product2);
            db.Products.Add(product3);
            db.Products.Add(product4);
            db.Products.Add(product5);
            #endregion

            #region Vegetables - category2
            Product product6 = new Product
            {
                Name = "Potato",
                Weight = 1,
                PrimeCost = 4,
                Price = 10.90m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-10),
                SellBy = DateTime.Now.AddDays(+2),
                AmountInStorage = 100,
                PhotoPath = "https://img3.zakaz.ua/src.1605015628.ad72436478c_2020-11-10_Victor/src.1605015628.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category2.Id,
                ProvisionerId = provisioner1.Id,
                SectionId = section1.Id
            };
            Product product7 = new Product
            {
                Name = "White cabbage",
                Weight = 1,
                PrimeCost = 2,
                Price = 4.90m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-10),
                SellBy = DateTime.Now.AddDays(+10),
                AmountInStorage = 10000,
                PhotoPath = "https://img3.zakaz.ua/src.1605015890.ad72436478c_2020-11-10_Victor/src.1605015890.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category2.Id,
                ProvisionerId = provisioner1.Id,
                SectionId = section1.Id
            };
            Product product8 = new Product
            {
                Name = "Carrots",
                Weight = 1,
                PrimeCost = 3.30m,
                Price = 6.90m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-10),
                SellBy = DateTime.Now.AddDays(+1),
                AmountInStorage = 90,
                PhotoPath = "https://img3.zakaz.ua/src.1605016531.ad72436478c_2020-11-10_Victor/src.1605016531.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category2.Id,
                ProvisionerId = provisioner1.Id,
                SectionId = section1.Id
            };
            Product product9 = new Product
            {
                Name = "Tomato",
                Weight = 1,
                PrimeCost = 20.30m,
                Price = 42.60m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-1),
                SellBy = DateTime.Now.AddDays(-1),
                AmountInStorage = 10000,
                PhotoPath = "https://img2.zakaz.ua/src.1605017085.ad72436478c_2020-11-10_Victor/src.1605017085.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category2.Id,
                ProvisionerId = provisioner1.Id,
                SectionId = section1.Id
            };
            Product product10 = new Product
            {
                Name = "Cucumber short-fruited",
                Weight = 1,
                PrimeCost = 30.30m,
                Price = 73.60m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-1),
                SellBy = DateTime.Now.AddDays(+10),
                AmountInStorage = 10000,
                PhotoPath = "https://img2.zakaz.ua/src.1605019211.ad72436478c_2020-11-10_Victor/src.1605019211.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category2.Id,
                ProvisionerId = provisioner1.Id,
                SectionId = section1.Id
            };

            db.Products.Add(product6);
            db.Products.Add(product7);
            db.Products.Add(product8);
            db.Products.Add(product9);
            db.Products.Add(product10);
            #endregion

            #region Juice - category3
            Product product11 = new Product
            {
                Name = "Sandora orange juice",
                Weight = 0.95,
                PrimeCost = 15.30m,
                Price = 31.60m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-2),
                SellBy = DateTime.Now.AddDays(+200),
                AmountInStorage = 20000,
                PhotoPath = "https://img3.zakaz.ua/1.1613037301.ad72436478c_2021-02-11_Tatyana_L/1.1613037301.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category3.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section2.Id
            };
            Product product12 = new Product
            {
                Name = "Nectar multifruit Our Juice",
                Weight = 0.95,
                PrimeCost = 10.30m,
                Price = 20.90m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-10),
                SellBy = DateTime.Now.AddDays(+200),
                AmountInStorage = 20000,
                PhotoPath = "https://img3.zakaz.ua/sik.1608985393.ad72436478c_2020-12-26_Tatyana_L/sik.1608985393.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category3.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section2.Id
            };
            Product product13 = new Product
            {
                Name = "Galicia apple juice",
                Weight = 1,
                PrimeCost = 20.30m,
                Price = 39.90m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-10),
                SellBy = DateTime.Now.AddDays(+200),
                AmountInStorage = 20000,
                PhotoPath = "https://img3.zakaz.ua/upload.version_1.0.e9bfcd0a36be8bb103bd7c6cdfcad98d.150x150.jpeg",
                CategoryId = category3.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section2.Id
            };
            Product product14 = new Product
            {
                Name = "Rich Tomato juice with pulp restored",
                Weight = 1,
                PrimeCost = 15.30m,
                Price = 25.50m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-10),
                SellBy = DateTime.Now.AddDays(+200),
                AmountInStorage = 20000,
                PhotoPath = "https://img2.zakaz.ua/15102020.1602597392.ad72436478c_2020-10-15_Tatyana_L/15102020.1602597392.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category3.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section2.Id
            };
            Product product15 = new Product
            {
                Name = "Jaffa juice 100% juice Multifruit Tropical fruit without added sugar",
                Weight = 1,
                PrimeCost = 18.30m,
                Price = 33.50m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-10),
                SellBy = DateTime.Now.AddDays(+200),
                AmountInStorage = 20000,
                PhotoPath = "https://img2.zakaz.ua/23022021.1614106042.ad72436478c_2021-02-23_Tatyana_L/23022021.1614106042.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category3.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section2.Id
            };

            db.Products.Add(product11);
            db.Products.Add(product12);
            db.Products.Add(product13);
            db.Products.Add(product14);
            db.Products.Add(product15);
            #endregion

            #region Cheese - category4
            Product product16 = new Product
            {
                Name = "Como Cheese Cream 50%",
                Weight = 1,
                PrimeCost = 120.30m,
                Price = 269.50m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-10),
                SellBy = DateTime.Now.AddDays(+10),
                AmountInStorage = 5000,
                PhotoPath = "https://img3.zakaz.ua/upload.version_1.0.7b7bc02c4ecd62f66fabaee9cf8d221c.150x150.jpeg",
                CategoryId = category4.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section3.Id
            };
            Product product17 = new Product
            {
                Name = "Hochland Almette cream cheese 35%",
                Weight = 0.15,
                PrimeCost = 15.30m,
                Price = 40.98m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-2),
                SellBy = DateTime.Now.AddDays(+10),
                AmountInStorage = 5000,
                PhotoPath = "https://img3.zakaz.ua/20180709.1530784613.ad72436478c_2018-07-09_Ucat/20180709.1530784613.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category4.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section3.Id
            };
            Product product18 = new Product
            {
                Name = "Galbani Mozzarella cheese 45%",
                Weight = 0.125,
                PrimeCost = 20.30m,
                Price = 51.57m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-10),
                SellBy = DateTime.Now.AddDays(+10),
                AmountInStorage = 5000,
                PhotoPath = "https://img3.zakaz.ua/m.1614766453.ad72436478c_2021-03-03_Svetlana/m.1614766453.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category4.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section3.Id
            };
            Product product19 = new Product
            {
                Name = "Spomlek Radamer hard cheese cut 45%",
                Weight = 0.15,
                PrimeCost = 15.30m,
                Price = 35.90m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-10),
                SellBy = DateTime.Now.AddDays(+10),
                AmountInStorage = 5000,
                PhotoPath = "https://img2.zakaz.ua/upload.version_1.0.559b1993077811b16d3609c528fa36b0.150x150.jpeg",
                CategoryId = category4.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section3.Id
            };
            Product product20 = new Product
            {
                Name = "Kaserei cheese Dorblyu 50%",
                Weight = 0.1,
                PrimeCost = 30.30m,
                Price = 65.90m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-10),
                SellBy = DateTime.Now.AddDays(+10),
                AmountInStorage = 5000,
                PhotoPath = "https://img3.zakaz.ua/src.1617009047.ad72436478c_2021-03-29_Tamara/src.1617009047.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category4.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section3.Id
            };

            db.Products.Add(product16);
            db.Products.Add(product17);
            db.Products.Add(product18);
            db.Products.Add(product19);
            db.Products.Add(product20);
            #endregion

            #region Meat - category5

            Product product21 = new Product
            {
                Name = "Bacon Yatran Spanish boiled and smoked premium",
                Weight = 1,
                PrimeCost = 105.30m,
                Price = 197.02m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+150),
                AmountInStorage = 4050,
                PhotoPath = "https://img3.zakaz.ua/upload.version_1.0.7646c471ee009db79fd2cd9228dc6c1b.150x150.jpeg",
                CategoryId = category5.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section3.Id
            };
            Product product22 = new Product
            {
                Name = "Ham Garmash Juicy smoked and boiled premium",
                Weight = 1,
                PrimeCost = 145.30m,
                Price = 257.02m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-2),
                SellBy = DateTime.Now.AddDays(+150),
                AmountInStorage = 4000,
                PhotoPath = "https://img2.zakaz.ua/20191028.1572258433.ad72436478c_2019-10-28_Tatiana/20191028.1572258433.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category5.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section3.Id
            };
            Product product23 = new Product
            {
                Name = "Ham Bashchynsky MK Marble boiled in",
                Weight = 0.15,
                PrimeCost = 30.30m,
                Price = 78.02m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+150),
                AmountInStorage = 4000,
                PhotoPath = "https://img2.zakaz.ua/upload.version_1.0.0d91bd67e221367b5e8aaf5cccb94ea0.150x150.jpeg",
                CategoryId = category5.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section3.Id
            };
            Product product24 = new Product
            {
                Name = "Balyk Ferax Special raw smoked",
                Weight = 1,
                PrimeCost = 150.30m,
                Price = 307.52m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+150),
                AmountInStorage = 4000,
                PhotoPath = "https://img2.zakaz.ua/upload.version_1.0.e1f97f0e104cb48bd1299541d8bc27b0.150x150.jpeg",
                CategoryId = category5.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section3.Id
            };
            Product product25 = new Product
            {
                Name = "Neck Yatran Favorite smoked",
                Weight = 1,
                PrimeCost = 200.30m,
                Price = 504.52m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+2),
                AmountInStorage = 14,
                PhotoPath = "https://img2.zakaz.ua/upload.version_1.0.68119825493a37563e4b6697adad386c.150x150.jpeg",
                CategoryId = category5.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section3.Id
            };
            db.Products.Add(product21);
            db.Products.Add(product22);
            db.Products.Add(product23);
            db.Products.Add(product24);
            db.Products.Add(product25);
            #endregion

            #region Wine - category6
            Product product26 = new Product
            {
                Name = "Wine Colonist Odessa Black dry red varietal aged 13.5%",
                Weight = 0.75,
                PrimeCost = 100.30m,
                Price = 210.52m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-1),
                SellBy = DateTime.Now.AddDays(+800),
                AmountInStorage = 5000,
                PhotoPath = "https://img3.zakaz.ua/done.1591020030.ad72436478c_2020-06-01_Svetlana/done.1591020030.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category6.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section4.Id
            };
            Product product27 = new Product
            {
                Name = "Wine Kartuli Vazi Alazani Valley red semisweet 11%",
                Weight = 0.75,
                PrimeCost = 70.30m,
                Price = 125.70m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+800),
                AmountInStorage = 5000,
                PhotoPath = "https://img2.zakaz.ua/20201218.1608230737.ad72436478c_2020-12-18_Auchan_Pavel_Alexey/20201218.1608230737.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category6.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section4.Id
            };
            Product product28 = new Product
            {
                Name = "White wine Louis Latour bourgogne dry 13%",
                Weight = 0.75,
                PrimeCost = 300.30m,
                Price = 580.70m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+800),
                AmountInStorage = 5000,
                PhotoPath = "https://img2.zakaz.ua/upload.version_1.0.bc61feccb7b0ea9e5c544a60248f1084.150x150.jpeg",
                CategoryId = category6.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section4.Id
            };
            Product product29 = new Product
            {
                Name = "Wine Mottura Stilio Primitivo di Manduria red 14.5%",
                Weight = 0.75,
                PrimeCost = 320.30m,
                Price = 580.70m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+800),
                AmountInStorage = 5000,
                PhotoPath = "https://img3.zakaz.ua/3.1601370489.ad72436478c_2020-09-29_Auchan_Pavel_Alexey/3.1601370489.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category6.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section4.Id
            };
            Product product30 = new Product
            {
                Name = "Wine Mud House Marlborough Sauvignon Blanc white dry 13%",
                Weight = 0.75,
                PrimeCost = 220.30m,
                Price = 441.20m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+800),
                AmountInStorage = 5000,
                PhotoPath = "https://img2.zakaz.ua/src.1580997031.ad72436478c_2020-02-06_Aliona/src.1580997031.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category6.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section4.Id
            };

            db.Products.Add(product26);
            db.Products.Add(product27);
            db.Products.Add(product28);
            db.Products.Add(product29);
            db.Products.Add(product30);
            #endregion

            #region Fish category7
            Product product31 = new Product
            {
                Name = "Norven salmon lightly salted fillet piece",
                Weight = 0.18,
                PrimeCost = 100.30m,
                Price = 191.20m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+100),
                AmountInStorage = 5,
                PhotoPath = "https://img2.zakaz.ua/src.1616419517.ad72436478c_2021-03-22_Tatyana/src.1616419517.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category7.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section3.Id
            };
            Product product32 = new Product
            {
                Name = "Herring Water World lightly salted fillet in oil",
                Weight = 0.25,
                PrimeCost = 20.30m,
                Price = 44.90m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+100),
                AmountInStorage = 10,
                PhotoPath = "https://img3.zakaz.ua/src.1602002125.ad72436478c_2020-10-06_YuliaT/src.1602002125.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category7.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section3.Id
            };
            Product product33 = new Product
            {
                Name = "Trout Master Fish fillet-piece lightly salted",
                Weight = 0.13,
                PrimeCost = 20.30m,
                Price = 44.90m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+100),
                AmountInStorage = 500,
                PhotoPath = "https://img3.zakaz.ua/src.1615821985.ad72436478c_2021-03-16_Tatyana/src.1615821985.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category7.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section3.Id
            };
            Product product34 = new Product
            {
                Name = "Mackerel Norven Lofoten lightly salted cartridge without head",
                Weight = 0.85,
                PrimeCost = 70.30m,
                Price = 149.55m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+100),
                AmountInStorage = 500,
                PhotoPath = "https://img3.zakaz.ua/upload.version_1.0.05316174643a0be4f98890d92a145b33.150x150.jpeg",
                CategoryId = category7.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section3.Id
            };
            Product product35 = new Product
            {
                Name = "Beer set Mermaid salmon lightly salted",
                Weight = 0.1,
                PrimeCost = 15.30m,
                Price = 32.55m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+100),
                AmountInStorage = 500,
                PhotoPath = "https://img3.zakaz.ua/src.1599666233.ad72436478c_2020-09-09_Alina/src.1599666233.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category7.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section3.Id
            };

            db.Products.Add(product31);
            db.Products.Add(product32);
            db.Products.Add(product33);
            db.Products.Add(product34);
            db.Products.Add(product35);
            #endregion

            #region Сereals - category8
            Product product36 = new Product
            {
                Name = "Buckwheat Every day the kernel is quickly boiled",
                Weight = 1,
                PrimeCost = 10.30m,
                Price = 37.55m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+300),
                AmountInStorage = 50,
                PhotoPath = "https://img2.zakaz.ua/20200528.1590593836.ad72436478c_2020-05-28_Auchan_Pavlo_Yulia_Alexey/20200528.1590593836.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category8.Id,
                ProvisionerId = provisioner1.Id,
                SectionId = section5.Id
            };
            Product product37 = new Product
            {
                Name = "Groats Every day wheat Poltava",
                Weight = 1,
                PrimeCost = 7.30m,
                Price = 16.55m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+300),
                AmountInStorage = 5,
                PhotoPath = "https://img2.zakaz.ua/zakaz-2-HP-630-Notebook-PC.1530696321.ad72436478c_2018-07-05_Sasha/zakaz-2-HP-630-Notebook-PC.1530696321.SNDDE581.obj.0.7.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category8.Id,
                ProvisionerId = provisioner1.Id,
                SectionId = section5.Id
            };
            Product product38 = new Product
            {
                Name = "Lentils One hundred poods Queen Lentils red small-seeded",
                Weight = 0.35,
                PrimeCost = 10.30m,
                Price = 28.05m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+300),
                AmountInStorage = 10,
                PhotoPath = "https://img3.zakaz.ua/upload.version_1.0.a8c30513c5f7360b694d6dea507c1321.150x150.jpeg",
                CategoryId = category8.Id,
                ProvisionerId = provisioner1.Id,
                SectionId = section5.Id
            };
            Product product39 = new Product
            {
                Name = "Oatmeal Olympus flattened",
                Weight = 0.5,
                PrimeCost = 6.30m,
                Price = 13.05m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+300),
                AmountInStorage = 40,
                PhotoPath = "https://img3.zakaz.ua/20200528.1590589012.ad72436478c_2020-05-28_Auchan_Pavlo_Yulia_Alexey/20200528.1590589012.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category8.Id,
                ProvisionerId = provisioner1.Id,
                SectionId = section5.Id
            };
            Product product40 = new Product
            {
                Name = "Cereal Olympus bulgur",
                Weight = 0.7,
                PrimeCost = 12.30m,
                Price = 25.15m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+300),
                AmountInStorage = 900,
                PhotoPath = "https://img2.zakaz.ua/upload.version_1.0.80f82871b274c4d432425faf9c24a8ae.150x150.jpeg",
                CategoryId = category8.Id,
                ProvisionerId = provisioner1.Id,
                SectionId = section5.Id
            };

            db.Products.Add(product36);
            db.Products.Add(product37);
            db.Products.Add(product38);
            db.Products.Add(product39);
            db.Products.Add(product40);
            #endregion

            #region Pasta - category9
            Product product41 = new Product
            {
                Name = "Pasta La Pasta Spaghetti",
                Weight = 0.7,
                PrimeCost = 18.30m,
                Price = 43.15m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+300),
                AmountInStorage = 60,
                PhotoPath = "https://img3.zakaz.ua/upload.version_1.0.bbb4fb20b242c801a5c9172b5d8313ac.150x150.jpeg",
                CategoryId = category9.Id,
                ProvisionerId = provisioner1.Id,
                SectionId = section5.Id
            };
            Product product42 = new Product
            {
                Name = "Pasta Chumak Spirals",
                Weight = 0.4,
                PrimeCost = 10.30m,
                Price = 19.25m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+300),
                AmountInStorage = 20,
                PhotoPath = "https://img3.zakaz.ua/04032021.1614768979.ad72436478c_2021-03-04_Tatyana_L/04032021.1614768979.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category9.Id,
                ProvisionerId = provisioner1.Id,
                SectionId = section5.Id
            };
            Product product43 = new Product
            {
                Name = "Pasta Barilla Farfalle №65",
                Weight = 0.5,
                PrimeCost = 13.30m,
                Price = 32.29m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+300),
                AmountInStorage = 600,
                PhotoPath = "https://img3.zakaz.ua/04082020.1596561101.ad72436478c_2020-08-04_Tatyana_L/04082020.1596561101.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category9.Id,
                ProvisionerId = provisioner1.Id,
                SectionId = section5.Id
            };
            Product product44 = new Product
            {
                Name = "Spaghetti pasta Barilla Spaghetti №3",
                Weight = 0.5,
                PrimeCost = 13.30m,
                Price = 32.09m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+300),
                AmountInStorage = 600,
                PhotoPath = "https://img2.zakaz.ua/04082020.1596565541.ad72436478c_2020-08-04_Tatyana_L/04082020.1596565541.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category9.Id,
                ProvisionerId = provisioner1.Id,
                SectionId = section5.Id
            };
            Product product45 = new Product
            {
                Name = "Buckwheat noodles JS Soba Noodles",
                Weight = 0.3,
                PrimeCost = 19.30m,
                Price = 44.21m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+300),
                AmountInStorage = 600,
                PhotoPath = "https://img2.zakaz.ua/20200623.1592890139.ad72436478c_2020-06-23_Auchan_Pavel_Alexey/20200623.1592890139.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category9.Id,
                ProvisionerId = provisioner1.Id,
                SectionId = section5.Id
            };

            db.Products.Add(product41);
            db.Products.Add(product42);
            db.Products.Add(product43);
            db.Products.Add(product44);
            db.Products.Add(product45);
            #endregion

            #region Sauces - category10
            Product product46 = new Product
            {
                Name = "Soy sauce TORCHIN Classic",
                Weight = 0.5,
                PrimeCost = 19.30m,
                Price = 42.21m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+100),
                AmountInStorage = 17,
                PhotoPath = "https://img2.zakaz.ua/1.1614540036.ad72436478c_2021-03-01_Tatyana_L/1.1614540036.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category10.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section6.Id
            };
            Product product47 = new Product
            {
                Name = "TORCHIN Tartar sauce",
                Weight = 0.2,
                PrimeCost = 10.30m,
                Price = 21.27m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+100),
                AmountInStorage = 50_000,
                PhotoPath = "https://img2.zakaz.ua/1.1614533989.ad72436478c_2021-03-01_Tatyana_L/1.1614533989.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category10.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section6.Id
            };
            Product product48 = new Product
            {
                Name = "Chumak Cranberry Sauce",
                Weight = 0.2,
                PrimeCost = 10.30m,
                Price = 22.27m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+100),
                AmountInStorage = 500,
                PhotoPath = "https://img2.zakaz.ua/12032021.1615550312.ad72436478c_2021-03-12_Tatyana_L/12032021.1615550312.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category10.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section6.Id
            };
            Product product49 = new Product
            {
                Name = "Kuhne Caesar salad dressing",
                Weight = 0.25,
                PrimeCost = 30.30m,
                Price = 60.90m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+100),
                AmountInStorage = 500,
                PhotoPath = "https://img3.zakaz.ua/upload.version_1.0.d79d5b765009747a54829bc96ab5dd3e.150x150.jpeg",
                CategoryId = category10.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section6.Id
            };
            Product product50 = new Product
            {
                Name = "Kuhne Balsamic Salad Sauce ",
                Weight = 0.25,
                PrimeCost = 30.30m,
                Price = 60.90m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+100),
                AmountInStorage = 500,
                PhotoPath = "https://img2.zakaz.ua/src.1594109628.ad72436478c_2020-07-07_Tamara/src.1594109628.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category10.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section6.Id
            };

            db.Products.Add(product46);
            db.Products.Add(product47);
            db.Products.Add(product48);
            db.Products.Add(product49);
            db.Products.Add(product50);
            #endregion

            #region Spice - category11
            Product product51 = new Product
            {
                Name = "Dream black pepper ground",
                Weight = 0.02,
                PrimeCost = 3.30m,
                Price = 8.90m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+500),
                AmountInStorage = 18,
                PhotoPath = "https://img2.zakaz.ua/src.1592844828.ad72436478c_2020-06-22_YuliaT/src.1592844828.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category11.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section7.Id
            };
            Product product52 = new Product
            {
                Name = "Paprika Eco ground",
                Weight = 0.02,
                PrimeCost = 1.30m,
                Price = 3.90m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+500),
                AmountInStorage = 1000,
                PhotoPath = "https://img2.zakaz.ua/upload.version_1.0.d3a3b91561ff3dcf8c15c772bf1ceb48.150x150.jpeg",
                CategoryId = category11.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section7.Id
            };
            Product product53 = new Product
            {
                Name = "Eco coriander ground",
                Weight = 0.02,
                PrimeCost = 1.30m,
                Price = 2.19m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+500),
                AmountInStorage = 1000,
                PhotoPath = "https://img3.zakaz.ua/20191224.1577184243.ad72436478c_2019-12-24_Tatiana/20191224.1577184243.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.150nowm.jpg.150x.jpg",
                CategoryId = category11.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section7.Id
            };
            Product product54 = new Product
            {
                Name = "Curry Eco gentle",
                Weight = 0.02,
                PrimeCost = 2.30m,
                Price = 5.61m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+500),
                AmountInStorage = 1000,
                PhotoPath = "https://img3.zakaz.ua/upload.version_1.0.5e6a99945b0151a6afb8aa6e03342d3a.150x150.jpeg",
                CategoryId = category11.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section7.Id
            };
            Product product55 = new Product
            {
                Name = "Seasoning Eco Italian herbs",
                Weight = 0.02,
                PrimeCost = 2.30m,
                Price = 5.80m,
                IsAvailable = true,
                ArrivalDate = DateTime.Now.AddDays(-5),
                SellBy = DateTime.Now.AddDays(+500),
                AmountInStorage = 1000,
                PhotoPath = "https://img3.zakaz.ua/upload.version_1.0.c8c839fb07c4832fddc5d837f7297e55.150x150.jpeg",
                CategoryId = category11.Id,
                ProvisionerId = provisioner2.Id,
                SectionId = section7.Id
            };

            db.Products.Add(product51);
            db.Products.Add(product52);
            db.Products.Add(product53);
            db.Products.Add(product54);
            db.Products.Add(product55);
            #endregion

            db.SaveChanges();
            #endregion

            #region Shops
            Shop shop1 = new Shop { Name = "ATB Market" };
            Shop shop2 = new Shop { Name = "Silpo" };
            Shop shop3 = new Shop { Name = "Auchan" };

            db.Shops.Add(shop1);
            db.Shops.Add(shop2);
            db.Shops.Add(shop3);

            db.SaveChanges();
            #endregion

            #region SoldProducts
            SoldProduct soldProduct3 = new SoldProduct
            {
                ProductId = product8.Id,
                ShopId = 1,
                Amount = 11500,
                SoldDate = DateTime.Now.AddDays(-3)
            };
            SoldProduct soldProduct5 = new SoldProduct
            {
                ProductId = product17.Id,
                ShopId = 3,
                Amount = 17100,
                SoldDate = DateTime.Now.AddDays(-15)
            };
            SoldProduct soldProduct6 = new SoldProduct
            {
                ProductId = product23.Id,
                ShopId = 3,
                Amount = 37000,
                SoldDate = DateTime.Now.AddDays(-25)
            };
            SoldProduct soldProduct7 = new SoldProduct
            {
                ProductId = product27.Id,
                ShopId = 3,
                Amount = 30000,
                SoldDate = DateTime.Now.AddDays(-45)
            };
            SoldProduct soldProduct8 = new SoldProduct
            {
                ProductId = product32.Id,
                ShopId = 3,
                Amount = 10000,
                SoldDate = DateTime.Now.AddDays(-55)
            };
            SoldProduct soldProduct9 = new SoldProduct
            {
                ProductId = product39.Id,
                ShopId = 2,
                Amount = 20000,
                SoldDate = DateTime.Now.AddDays(-75)
            };
            SoldProduct soldProduct10 = new SoldProduct
            {
                ProductId = product41.Id,
                ShopId = 2,
                Amount = 28000,
                SoldDate = DateTime.Now.AddDays(-85)
            };
            SoldProduct soldProduct11 = new SoldProduct
            {
                ProductId = product51.Id,
                ShopId = 2,
                Amount = 7280,
                SoldDate = DateTime.Now.AddDays(-115)
            };
            SoldProduct soldProduct12 = new SoldProduct
            {
                ProductId = product53.Id,
                ShopId = 2,
                Amount = 9980,
                SoldDate = DateTime.Now.AddDays(-215)
            };
            SoldProduct soldProduct2 = new SoldProduct
            {
                ProductId = product1.Id,
                ShopId = 1,
                Amount = 20_300,
                SoldDate = DateTime.Now.AddDays(-5)
            };
            SoldProduct soldProduct13 = new SoldProduct
            {
                ProductId = product1.Id,
                ShopId = 2,
                Amount = 80_003,
                SoldDate = DateTime.Now.AddDays(-215)
            };
            SoldProduct soldProduct14 = new SoldProduct
            {
                ProductId = product2.Id,
                ShopId = 1,
                Amount = 25_000,
                SoldDate = DateTime.Now.AddDays(-15)
            };
            SoldProduct soldProduct15 = new SoldProduct
            {
                ProductId = product2.Id,
                ShopId = 2,
                Amount = 30_000,
                SoldDate = DateTime.Now.AddDays(-25)
            };
            SoldProduct soldProduct4 = new SoldProduct
            {
                ProductId = product3.Id,
                ShopId = 3,
                Amount = 10_700,
                SoldDate = DateTime.Now.AddDays(-3)
            };
            SoldProduct soldProduct1 = new SoldProduct
            {
                ProductId = product3.Id,
                ShopId = 1,
                Amount = 10_200,
                SoldDate = DateTime.Now.AddDays(-5)
            };
            SoldProduct soldProduct16 = new SoldProduct
            {
                ProductId = product4.Id,
                ShopId = 2,
                Amount = 5_000,
                SoldDate = DateTime.Now.AddDays(-215)
            };
            SoldProduct soldProduct17 = new SoldProduct
            {
                ProductId = product4.Id,
                ShopId = 2,
                Amount = 60_000,
                SoldDate = DateTime.Now.AddDays(-215)
            };
            SoldProduct soldProduct18 = new SoldProduct
            {
                ProductId = product5.Id,
                ShopId = 2,
                Amount = 20_000,
                SoldDate = DateTime.Now.AddDays(-115)
            };
            SoldProduct soldProduct19 = new SoldProduct
            {
                ProductId = product5.Id,
                ShopId = 3,
                Amount = 40_000,
                SoldDate = DateTime.Now.AddDays(-115)
            };
            SoldProduct soldProduct20 = new SoldProduct
            {
                ProductId = product46.Id,
                ShopId = 3,
                Amount = 40_000,
                SoldDate = DateTime.Now.AddDays(-115)
            };
            SoldProduct soldProduct21 = new SoldProduct
            {
                ProductId = product12.Id,
                ShopId = 3,
                Amount = 127100,
                SoldDate = DateTime.Now.AddDays(-15)
            };
            SoldProduct soldProduct22 = new SoldProduct
            {
                ProductId = product29.Id,
                ShopId = 1,
                Amount = 117100,
                SoldDate = DateTime.Now.AddDays(-15)
            };



            db.SoldProducts.Add(soldProduct1);
            db.SoldProducts.Add(soldProduct2);
            db.SoldProducts.Add(soldProduct3);
            db.SoldProducts.Add(soldProduct4);
            db.SoldProducts.Add(soldProduct5);
            db.SoldProducts.Add(soldProduct6);
            db.SoldProducts.Add(soldProduct7);
            db.SoldProducts.Add(soldProduct8);
            db.SoldProducts.Add(soldProduct9);
            db.SoldProducts.Add(soldProduct10);
            db.SoldProducts.Add(soldProduct11);
            db.SoldProducts.Add(soldProduct12);
            db.SoldProducts.Add(soldProduct13);
            db.SoldProducts.Add(soldProduct14);
            db.SoldProducts.Add(soldProduct15);
            db.SoldProducts.Add(soldProduct16);
            db.SoldProducts.Add(soldProduct17);
            db.SoldProducts.Add(soldProduct18);
            db.SoldProducts.Add(soldProduct19);
            db.SoldProducts.Add(soldProduct20);
            db.SoldProducts.Add(soldProduct21);
            db.SoldProducts.Add(soldProduct22);

            db.SaveChanges();
            #endregion

            #region Set Rating for products, SelectionLabel, Change photos

            // prepare folder to save local products photos
            if (!Directory.Exists(Settings.DownloadImagesFolder))
            {
                Directory.CreateDirectory(Settings.DownloadImagesFolder);
            }
            else
            {
                ClearDirectory(Settings.DownloadImagesFolder);
            }

            var products = ProductsRepository.GetAllSync();
            foreach (var product in products)
            {
                // 1. Set Rating. Rating depends on Amount of product sales (automatically setting)
                int salesAmount = SoldProductsRepository.GetGeneralAmountProductSalesById(product.Id);
                product.Rating = SoldProductsRepository.SetProductRating(salesAmount);

                // 2. Set SelectionLabel to string.Empty to avoid problems, when user will select product in main products list
                product.SelectionLabel = string.Empty;

                // 3. download images (from some remote server, for example) to computer memory while DB init to increase application performance while running. DB init will be only once, but application will be work faster all time
                ChangeProductImageToLocal(product, Settings.DownloadImagesFolder);
            }
            #endregion

            #region WroteOffProducts
            WroteOffProduct wroteOffProduct1 = new WroteOffProduct
            {
                ProductId = product5.Id,
                Amount = 30,
                Date = DateTime.Now.AddDays(-6)
            };
            WroteOffProduct wroteOffProduct2 = new WroteOffProduct
            {
                ProductId = product8.Id,
                Amount = 10,
                Date = DateTime.Now.AddDays(-16)
            };
            WroteOffProduct wroteOffProduct3 = new WroteOffProduct
            {
                ProductId = product13.Id,
                Amount = 20,
                Date = DateTime.Now.AddDays(-26)
            };
            WroteOffProduct wroteOffProduct4 = new WroteOffProduct
            {
                ProductId = product14.Id,
                Amount = 5,
                Date = DateTime.Now.AddDays(-27)
            };
            WroteOffProduct wroteOffProduct5 = new WroteOffProduct
            {
                ProductId = product22.Id,
                Amount = 10,
                Date = DateTime.Now.AddDays(-30)
            };
            WroteOffProduct wroteOffProduct6 = new WroteOffProduct
            {
                ProductId = product25.Id,
                Amount = 7,
                Date = DateTime.Now.AddDays(-40)
            };
            WroteOffProduct wroteOffProduct7 = new WroteOffProduct
            {
                ProductId = product31.Id,
                Amount = 2,
                Date = DateTime.Now.AddDays(-55)
            };
            WroteOffProduct wroteOffProduct8 = new WroteOffProduct
            {
                ProductId = product38.Id,
                Amount = 2,
                Date = DateTime.Now.AddDays(-70)
            };
            WroteOffProduct wroteOffProduct9 = new WroteOffProduct
            {
                ProductId = product44.Id,
                Amount = 3,
                Date = DateTime.Now.AddDays(-90)
            };
            WroteOffProduct wroteOffProduct10 = new WroteOffProduct
            {
                ProductId = product50.Id,
                Amount = 9,
                Date = DateTime.Now.AddDays(-110)
            };
            WroteOffProduct wroteOffProduct11 = new WroteOffProduct
            {
                ProductId = product51.Id,
                Amount = 11,
                Date = DateTime.Now.AddDays(-130)
            };
            WroteOffProduct wroteOffProduct12 = new WroteOffProduct
            {
                ProductId = product52.Id,
                Amount = 1,
                Date = DateTime.Now.AddDays(-140)
            };

            db.WroteOffProducts.Add(wroteOffProduct1);
            db.WroteOffProducts.Add(wroteOffProduct2);
            db.WroteOffProducts.Add(wroteOffProduct3);
            db.WroteOffProducts.Add(wroteOffProduct4);
            db.WroteOffProducts.Add(wroteOffProduct5);
            db.WroteOffProducts.Add(wroteOffProduct6);
            db.WroteOffProducts.Add(wroteOffProduct7);
            db.WroteOffProducts.Add(wroteOffProduct8);
            db.WroteOffProducts.Add(wroteOffProduct9);
            db.WroteOffProducts.Add(wroteOffProduct10);
            db.WroteOffProducts.Add(wroteOffProduct11);
            db.WroteOffProducts.Add(wroteOffProduct12);
            #endregion

            #region Users
            User user1 = new User
            {
                Login = "admin",
                Password = "admin",
                IsAdmin = true,
                Label = "admin"
            };
            User user2 = new User
            {
                Login = "igor",
                Password = "1111",
                IsAdmin = false
            };

            db.Users.Add(user1);
            db.Users.Add(user2);
            #endregion

            db.SaveChanges();
        }

        #region Change products images helper methods
        private void ChangeProductImageToLocal(Product product, string downloadImagesFolder)
        {
            WebClient wc = new WebClient();
            string filename = Path.GetFileName(product.PhotoPath);
            string extension = Path.GetExtension(filename);
            filename = $"{Guid.NewGuid()}{extension}";

            try
            {
                string savePath = Path.GetFullPath(Path.Combine(downloadImagesFolder, filename));
                Uri uriAddress = new Uri(product.PhotoPath);
                wc.DownloadFileAsync(uriAddress, savePath);
                product.PhotoPath = savePath;

            }
            catch (Exception)
            {
                product.PhotoPath = Settings.DefaultImagePath;
            }
        }

        private void ClearDirectory(string path)
        {
            string[] allFiles = Directory.GetFiles(path);
            if (allFiles.Length > 0)
            {
                foreach (var file in allFiles)
                {
                    File.Delete(file);
                }
            }
        }

        #endregion

        public void Save()
        {
            db.SaveChanges();
        }

        bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }
    }
}
