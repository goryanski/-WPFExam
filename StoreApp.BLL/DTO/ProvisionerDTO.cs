using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.BLL.DTO.Warehouse;

namespace StoreApp.BLL.DTO
{
    public class ProvisionerDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
