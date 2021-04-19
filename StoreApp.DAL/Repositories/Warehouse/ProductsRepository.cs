using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreApp.DAL.EF;
using StoreApp.DAL.Entities.Warehouse;

namespace StoreApp.DAL.Repositories.Warehouse
{
    public class ProductsRepository : BaseRepository<Product>
    {
        public ProductsRepository(StoreContext context) : base(context)
        {
        }

        public override async Task Update(Product entity)
        {
            //await Task.Run( async () => 
            //{

            //});
            // get entity from DB
            var srchEntity = await Get(entity.Id);
            // change entity
            ;
            srchEntity.Name = entity.Name;
            srchEntity.Weight = entity.Weight;
            srchEntity.PrimeCost = entity.PrimeCost;
            srchEntity.Price = entity.Price;
            srchEntity.IsAvailable = entity.IsAvailable;
            srchEntity.ArrivalDate = entity.ArrivalDate;
            srchEntity.SellBy = entity.SellBy;
            srchEntity.AmountInStorage = entity.AmountInStorage;
            srchEntity.Rating = entity.Rating;
            srchEntity.PhotoPath = entity.PhotoPath;
            srchEntity.SelectionLabel = entity.SelectionLabel;
            //srchEntity.Category = entity.Category;
            srchEntity.CategoryId = entity.CategoryId;
            //srchEntity.Provisioner = entity.Provisioner;
            srchEntity.ProvisionerId = entity.ProvisionerId;
            //srchEntity.Section = entity.Section;
            srchEntity.SectionId = entity.SectionId;
            // change entity state
            ;
            db.Entry(srchEntity).State = EntityState.Modified;
            // save changes
            await db.SaveChangesAsync();
            ;
        }

        public override async Task<List<Product>> GetAll(Func<Product, bool> predicate)
        {
            return await Task.Run(() => Table
            .Where(p => p.IsAvailable == true)
            .Where(predicate).ToList());
        }

        public async Task<Product> GetFullData(int id)
        {
            return await db.Set<Product>()
                .Include(p => p.Category)
                .Include(p => p.Provisioner)
                .Include(p => p.Section)
                .FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public List<Product> GetAllSync()
        {
            return Table.ToList();
        }

        public async Task UpdateProductCount(Product entity, int newValue)
        {
            var srchEntity = await Get(entity.Id);
            srchEntity.AmountInStorage = newValue;
            db.Entry(srchEntity).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteProduct(Product entity)
        {
            var srchEntity = await Get(entity.Id);
            srchEntity.IsAvailable = false;
            db.Entry(srchEntity).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
