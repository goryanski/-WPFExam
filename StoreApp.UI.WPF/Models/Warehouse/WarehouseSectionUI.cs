using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.UI.WPF.Models.Warehouse
{
    public class WarehouseSectionUI : BaseModel
    {
        public int SectionNumber { get; set; }
        public List<ProductUI> Products { get; set; }
    }
}
