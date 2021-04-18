using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.DAL.EF;
using StoreApp.DAL.Entities.ExtraTables;

namespace StoreApp.DAL.Repositories.ExtraTables
{
    public class WroteOffProductsRepository : BaseRepository<WroteOffProduct>
    {
        public WroteOffProductsRepository(StoreContext context) : base(context)
        {
        }

        public override async Task Update(WroteOffProduct entity)
        {
        }
    }
}
