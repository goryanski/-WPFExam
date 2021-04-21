using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreApp.DAL.EF;
using StoreApp.DAL.Entities;

namespace StoreApp.DAL.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(StoreContext context) : base(context)
        {
        }

        public override async Task Update(Order entity)
        {
        }
    }
}
