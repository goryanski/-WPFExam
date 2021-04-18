using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.DAL.Entities.Warehouse;

namespace StoreApp.DAL.Entities
{
    public class Provisioner : BaseEntity
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public List<Product> Products { get; set; }
    }
}
