using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.DAL.Entities.Warehouse
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
