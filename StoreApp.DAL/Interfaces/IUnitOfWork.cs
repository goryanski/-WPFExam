using StoreApp.DAL.Entities;
using StoreApp.DAL.Repositories;
using StoreApp.DAL.Repositories.ExtraTables;
using StoreApp.DAL.Repositories.Warehouse;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        SoldProductsRepository SoldProductsRepository { get; }
        WroteOffProductsRepository WroteOffProductsRepository { get; }
        CategoriesRepository CategoriesRepository { get; }
        ProductsRepository ProductsRepository { get; }
        WarehouseSectionsRepository WarehouseSectionsRepository { get; }
        ProvisionersRepository ProvisionersRepository { get; }
        UsersRepository UsersRepository { get; }
        ShopsRepository ShopsRepository { get; }

        void Save();
    }
}
