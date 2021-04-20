using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.BLL.DTO
{
    public class OrderDTO : BaseDTO
    {
        public string ProductName { get; set; }
        public int ProvisionerId { get; set; }
        public int CountToOrder { get; set; }
    }
}
