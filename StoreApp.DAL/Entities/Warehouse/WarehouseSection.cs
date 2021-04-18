using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.DAL.Entities.Warehouse
{
    // Where product is in the warehouse
    public class WarehouseSection : BaseEntity
    {
        public int SectionNumber { get; set; }
        public List<Product> Products { get; set; }
    }
}
