using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.BLL.DTO.Warehouse
{
    public class WarehouseSectionDTO : BaseDTO
    {
        public int SectionNumber { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
