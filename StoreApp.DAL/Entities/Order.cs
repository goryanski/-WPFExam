using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.DAL.Entities
{
    public class Order : BaseEntity
    {
        public string ProductName { get; set; }
        public int ProvisionerId { get; set; }
        public int CountToOrder { get; set; }
    }
}
