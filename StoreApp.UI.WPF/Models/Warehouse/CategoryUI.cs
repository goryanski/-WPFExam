using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.UI.WPF.Models.Warehouse
{
    public class CategoryUI : BaseModel
    {
        public string Name { get; set; }
        public List<ProductUI> Products { get; set; }
    }
}

