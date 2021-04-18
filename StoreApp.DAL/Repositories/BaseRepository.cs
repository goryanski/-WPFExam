using Microsoft.EntityFrameworkCore;
using StoreApp.DAL.EF;
using StoreApp.DAL.Entities;
using StoreApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.DAL.Repositories
{
    public abstract class BaseRepository<T> : IRepositoryAsync<T> 
        where T : BaseEntity
    {
        protected StoreContext db;
        protected DbSet<T> Table => db.Set<T>();
        public BaseRepository(StoreContext context)
        {
            db = context;
        }

        public virtual async Task Create(T entity)
        {
            db.Entry(entity).State = EntityState.Added;
            await db.SaveChangesAsync();
        }
        public async Task<T> Get(int id)
        {
            return await Table.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await Table.ToListAsync();
        }

        public virtual async Task<List<T>> GetAll(Func<T, bool> predicate)
        {
            return await Task.Run(() => Table.Where(predicate).ToList());
        }

        public async Task Delete(int id)
        {
            var entity = await Get(id);
            db.Entry(entity).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }

        public abstract Task Update(T entity);
    }
}
