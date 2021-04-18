using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.DAL.EF;
using StoreApp.DAL.Entities.Warehouse;

namespace StoreApp.DAL.Repositories.Warehouse
{
    public class CategoriesRepository : BaseRepository<Category>
    {
        public CategoriesRepository(StoreContext context) : base(context)
        {
        }

        public override async Task Update(Category entity)
        {
        }
    }
}
