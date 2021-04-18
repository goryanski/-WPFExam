using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.UI.WPF.Models.Warehouse;

namespace StoreApp.UI.WPF.Models
{
    public class ProvisionerUI : BaseModel
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public List<ProductUI> Products { get; set; }
    }
}
