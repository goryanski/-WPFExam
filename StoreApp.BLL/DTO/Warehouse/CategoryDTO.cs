using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.BLL.DTO.Warehouse
{
    public class CategoryDTO : BaseDTO
    {
        public string Name { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
