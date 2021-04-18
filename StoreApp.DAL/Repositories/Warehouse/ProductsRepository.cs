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
