using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.DAL.EF;
using StoreApp.DAL.Entities;

namespace StoreApp.DAL.Repositories
{
    public class UsersRepository : BaseRepository<User>
    {
        public UsersRepository(StoreContext context) : base(context)
        {
        }

        public override async Task Update(User entity)
        {
            // get entity from DB
            var srchEntity = await Get(entity.Id);
            // change entity
            srchEntity.Login = entity.Login;
            srchEntity.Password = entity.Password;
            // change entity state
            db.Entry(srchEntity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            // save changes
            await db.SaveChangesAsync();
        }
    }
}
