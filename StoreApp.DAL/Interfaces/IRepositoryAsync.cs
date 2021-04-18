using StoreApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.DAL.Interfaces
{
    public interface IRepositoryAsync<T> 
        where T : BaseEntity
    {        
        Task<T> Get(int id);
        Task<List<T>> GetAll();
        Task<List<T>> GetAll(Func<T, bool> predicate);
        Task Create(T entity);
        Task Delete(int id);
        Task Update(T entity);
    }
}
