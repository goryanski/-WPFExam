using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.UI.WPF.Models
{
    public class OrderUI : BaseModel
    {
        public string ProductName { get; set; }
        public int ProvisionerId { get; set; }
        public int CountToOrder { get; set; }
    }
}
