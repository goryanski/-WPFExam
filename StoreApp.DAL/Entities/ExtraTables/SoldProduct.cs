using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.DAL.Entities.ExtraTables
{
    // Sent to stores = sold
    public class SoldProduct : BaseEntity
    {
        public int ProductId { get; set; }
        public int ShopId { get; set; }
        public int Amount { get; set; }
        public DateTime SoldDate { get; set; }
    }
}
