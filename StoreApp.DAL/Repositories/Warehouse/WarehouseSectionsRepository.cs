using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.DAL.EF;
using StoreApp.DAL.Entities.Warehouse;

namespace StoreApp.DAL.Repositories.Warehouse
{
    public class WarehouseSectionsRepository : BaseRepository<WarehouseSection>
    {
        public WarehouseSectionsRepository(StoreContext context) : base(context)
        {
        }

        public override async Task Update(WarehouseSection entity)
        {
        }
    }
}
